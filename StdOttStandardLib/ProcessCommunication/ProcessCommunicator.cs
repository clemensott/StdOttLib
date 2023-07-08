using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.ProcessCommunication
{
    public abstract class ProcessCommunicator : IDisposable
    {
        private const int timerIntervall = 100;
        private const string receivedCmdName = "cmd_received";

        protected readonly IProcessComandPersisting persisting;
        private readonly int maxCmdCount;
        private readonly List<ProcessCommandInfo> sendCMDs;
        private readonly Dictionary<string, string> keysDict;
        private readonly HashSet<string> receivedCMDs;
        private readonly Timer writeTime, readTimer;
        private readonly SemaphoreSlim semWriteLoop, semReadLoop;
        private string[] lastWriteCommandIds;

        public ProcessCommunicator(IProcessComandPersisting persisting, int maxCmdCount = 100)
        {
            this.persisting = persisting;
            this.maxCmdCount = maxCmdCount;

            sendCMDs = new List<ProcessCommandInfo>();
            keysDict = new Dictionary<string, string>();
            receivedCMDs = new HashSet<string>();
            writeTime = new Timer(new TimerCallback(WriteLoop), null, Timeout.Infinite, 1);
            readTimer = new Timer(new TimerCallback(ReadLoop), null, Timeout.Infinite, 1);
            semWriteLoop = new SemaphoreSlim(1);
            semReadLoop = new SemaphoreSlim(1);
            lastWriteCommandIds = new string[0];
        }

        private static string ToString(ProcessCommandInfo cmd)
        {
            int dataNewLineIndex = cmd.Data.IndexOf('\n');
            string data = dataNewLineIndex == -1 ? cmd.Data : (cmd.Data.Remove(dataNewLineIndex) + "...");
            return $"id={cmd.ID} name={cmd.Name} data={data}";
        }

        private void AppendCommand(string name, string data, string key)
        {
            ProcessCommandInfo cmd = new ProcessCommandInfo(name, data);
            //System.Diagnostics.Debug.WriteLine($"append cmd1: per={persisting.GetHashCode()} {ToString(cmd)} key={key}");
            lock (sendCMDs)
            {
                if (key != null)
                {
                    string keyCmdId;
                    if (keysDict.TryGetValue(key, out keyCmdId))
                    {
                        //System.Diagnostics.Debug.WriteLine($"append cmd2: per={persisting.GetHashCode()} keyCmdId={keyCmdId}");
                        RemoveCommandById(keyCmdId);
                    }

                    keysDict[key] = cmd.ID;
                }
                sendCMDs.Add(cmd);
            }
        }

        private void RemoveCommandById(string id)
        {
            lock (sendCMDs)
            {
                int index = sendCMDs.FindIndex(cmd => cmd.ID == id);
                //System.Diagnostics.Debug.WriteLine($"remove cmd index: per={persisting.GetHashCode()} index={index}");
                if (index != -1) sendCMDs.RemoveAt(index);
            }
        }

        private void CleanKeysDict()
        {
            lock (sendCMDs)
            {
                foreach (KeyValuePair<string, string> pair in keysDict.Where(p => !sendCMDs.Any(c => c.ID == p.Value)).ToArray())
                {
                    keysDict.Remove(pair.Key);
                }
            }
        }

        private async void WriteLoop(object state)
        {
            if (semWriteLoop.CurrentCount == 0) return;
            await semWriteLoop.WaitAsync();

            try
            {
                ProcessCommandInfo[] writeCMDs;
                lock (sendCMDs)
                {
                    //System.Diagnostics.Debug.WriteLine($"write cmd1: per={persisting.GetHashCode()} sendCount={sendCMDs.Count}{string.Concat(sendCMDs.Select(cmd => $"\n{ToString(cmd)}"))}");
                    writeCMDs = (maxCmdCount > 0 ? sendCMDs.Take(maxCmdCount) : sendCMDs).ToArray();
                }

                string[] writeCmdIds = writeCMDs.Select(c => c.ID).ToArray();
                if (!writeCmdIds.SequenceEqual(lastWriteCommandIds))
                {
                    await persisting.WriteCommands(writeCMDs.ToArray());
                    lastWriteCommandIds = writeCmdIds;
                }
                //System.Diagnostics.Debug.WriteLine($"write cmd2: per={persisting.GetHashCode()} writeCount={writeCMDs.Length}");

                CleanKeysDict();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"write loop error: per={persisting.GetHashCode()} e={e}");
            }
            finally
            {
                semWriteLoop.Release();
            }
        }

        private async void ReadLoop(object state)
        {
            //System.Diagnostics.Debug.WriteLine($"loop0: per={persisting.GetHashCode()} semLoop={semLoop.CurrentCount}");
            if (semReadLoop.CurrentCount == 0) return;
            await semReadLoop.WaitAsync();
            try
            {
                //System.Diagnostics.Debug.WriteLine($"loop1: per={persisting.GetHashCode()}");
                ProcessCommandInfo[] cmds = await persisting.ReadCommands();
                if (cmds == null) return;

                //System.Diagnostics.Debug.WriteLine($"loop3: per={persisting.GetHashCode()} cmds={cmds.Length}");
                foreach (ProcessCommandInfo cmd in cmds)
                {
                    //System.Diagnostics.Debug.WriteLine($"loop cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                    if (receivedCMDs.Contains(cmd.ID)) continue;

                    receivedCMDs.Add(cmd.ID);

                    //System.Diagnostics.Debug.WriteLine($"process cmd: per={persisting.GetHashCode()} {ToString(cmd)}");

                    if (cmd.Name == receivedCmdName)
                    {
                        //System.Diagnostics.Debug.WriteLine($"remove cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                        RemoveCommandById(cmd.Data);
                        continue;
                    }

                    AppendCommand(receivedCmdName, cmd.ID, null);
                    try
                    {
                        ReceiveCommand(new ReceivedProcessCommand(cmd.Name, cmd.Data));
                    }
                    catch { }
                }

                //System.Diagnostics.Debug.WriteLine($"loop6: per={persisting.GetHashCode()}");

                ProcessCommandInfo[] removedCMDs;
                lock (sendCMDs)
                {
                    IEnumerable<string> cmdIDs = cmds.Select(cmd => cmd.ID);
                    removedCMDs = sendCMDs.Where(cmd => cmd.Name == receivedCmdName && !cmdIDs.Contains(cmd.Data)).ToArray();
                }
                foreach (ProcessCommandInfo cmd in removedCMDs)
                {
                    //System.Diagnostics.Debug.WriteLine($"remove received cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                    RemoveCommandById(cmd.ID);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"loop error: per={persisting.GetHashCode()} e={e}");
            }
            finally
            {
                semReadLoop.Release(1);
            }
        }

        protected abstract void ReceiveCommand(ReceivedProcessCommand cmd);

        protected void StartTimer()
        {
            System.Diagnostics.Debug.WriteLine($"StartTimer per={persisting.GetHashCode()} now={DateTime.Now.TimeOfDay}");
            writeTime.Change(0, 100);
            readTimer.Change(0, 100);
        }

        protected void StopTimer()
        {
            writeTime.Change(Timeout.Infinite, 100);
            readTimer.Change(Timeout.Infinite, 100);
        }

        protected void SendKeyCommand(string name)
        {
            SendCommand(name, name);
        }

        protected void SendCommand(string name, string key = null)
        {
            SendDataCommand(name, "", key);
        }

        protected void SendDataCommand(string name, object data, string key = null)
        {
            string xml = StdUtils.XmlSerialize(data);
            SendDataCommand(name, xml, key);
        }

        protected void SendDataCommand(string name, string data, string key = null)
        {
            AppendCommand(name, data, key);
        }

        protected void RemoveCommandByKey(string key)
        {
            lock (sendCMDs)
            {
                string cmdId;
                if (keysDict.TryGetValue(key, out cmdId))
                {
                    RemoveCommandById(cmdId);
                    keysDict.Remove(key);
                }
            }
        }

        protected Task FlushAllCommands()
        {
            //WriteCommands(true);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            writeTime.Dispose();
            readTimer.Dispose();
        }
    }
}
