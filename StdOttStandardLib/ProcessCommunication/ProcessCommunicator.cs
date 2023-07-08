using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.ProcessCommunication
{
    public abstract class ProcessCommunicator : IDisposable
    {
        private const string receivedCmdName = "cmd_received";

        protected readonly IProcessComandPersisting persisting;
        private readonly int maxCmdCount;
        private readonly List<ProcessCommandInfo> sendCMDs;
        private readonly Dictionary<string, string> keysDict;
        private readonly HashSet<string> receivedCMDs;
        private readonly Timer timer;
        private readonly SemaphoreSlim semWrite, semLoop;

        public ProcessCommunicator(IProcessComandPersisting persisting, int maxCmdCount = 100)
        {
            this.persisting = persisting;
            this.maxCmdCount = maxCmdCount;

            sendCMDs = new List<ProcessCommandInfo>();
            keysDict = new Dictionary<string, string>();
            receivedCMDs = new HashSet<string>();
            timer = new Timer(new TimerCallback(Loop), null, Timeout.Infinite, 1);
            semWrite = new SemaphoreSlim(1);
            semLoop = new SemaphoreSlim(1);
        }

        private static string ToString(ProcessCommandInfo cmd)
        {
            int dataNewLineIndex = cmd.Data.IndexOf('\n');
            string data = dataNewLineIndex == -1 ? cmd.Data : (cmd.Data.Remove(dataNewLineIndex) + "...");
            return $"id={cmd.ID} name={cmd.Name} data={data}";
        }

        private async Task WriteCommands()
        {
            await semWrite.WaitAsync();
            try
            {
                ProcessCommandInfo[] writeCMDs;
                lock (sendCMDs)
                {
                    System.Diagnostics.Debug.WriteLine($"write cmd1: per={persisting.GetHashCode()} sendCount={sendCMDs.Count}{string.Concat(sendCMDs.Select(cmd => $"\n{ToString(cmd)}"))}");
                    writeCMDs = (maxCmdCount > 0 ? sendCMDs.Take(maxCmdCount) : sendCMDs).ToArray();
                }

                await persisting.WriteCommands(writeCMDs.ToArray());
                System.Diagnostics.Debug.WriteLine($"write cmd2: per={persisting.GetHashCode()} writeCount={writeCMDs.Length}");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"write cmd error: per={persisting.GetHashCode()} e={e}");
            }
            finally
            {
                semWrite.Release();
            }
        }

        private void AppendCommand(string name, string data, string key)
        {
            ProcessCommandInfo cmd = new ProcessCommandInfo(name, data);
            System.Diagnostics.Debug.WriteLine($"append cmd1: per={persisting.GetHashCode()} {ToString(cmd)} key={key}");
            lock (sendCMDs)
            {
                if (key != null)
                {
                    string keyCmdId;
                    if (keysDict.TryGetValue(key, out keyCmdId))
                    {
                        System.Diagnostics.Debug.WriteLine($"append cmd2: per={persisting.GetHashCode()} keyCmdId={keyCmdId}");
                        RemoveCommand(keyCmdId);
                    }

                    keysDict[key] = cmd.ID;
                }
                sendCMDs.Add(cmd);
            }
        }

        private void RemoveCommand(string id)
        {
            lock (sendCMDs)
            {
                int index = sendCMDs.FindIndex(cmd => cmd.ID == id);
                System.Diagnostics.Debug.WriteLine($"remove cmd index: per={persisting.GetHashCode()} index={index}");
                if (index != -1) sendCMDs.RemoveAt(index);
                if (keysDict.ContainsKey(id)) keysDict.Remove(id);
            }
        }

        private async void Loop(object state)
        {
            //System.Diagnostics.Debug.WriteLine($"loop0: per={persisting.GetHashCode()} semLoop={semLoop.CurrentCount}");
            if (semLoop.CurrentCount == 0) return;
            await semLoop.WaitAsync();
            try
            {
                //System.Diagnostics.Debug.WriteLine($"loop1: per={persisting.GetHashCode()}");
                ProcessCommandInfo[] cmds = await persisting.ReadCommands();
                if (cmds == null) return;

                bool changedCommands = false;
                System.Diagnostics.Debug.WriteLine($"loop3: per={persisting.GetHashCode()} cmds={cmds.Length}");
                foreach (ProcessCommandInfo cmd in cmds)
                {
                    System.Diagnostics.Debug.WriteLine($"loop cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                    if (receivedCMDs.Contains(cmd.ID)) continue;

                    receivedCMDs.Add(cmd.ID);
                    changedCommands = true;

                    System.Diagnostics.Debug.WriteLine($"process cmd: per={persisting.GetHashCode()} {ToString(cmd)}");

                    if (cmd.Name == receivedCmdName)
                    {
                        System.Diagnostics.Debug.WriteLine($"remove cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                        RemoveCommand(cmd.Data);
                        continue;
                    }

                    AppendCommand(receivedCmdName, cmd.ID, null);
                    try
                    {
                        ReceiveCommand(new ReceivedProcessCommand(cmd.Name, cmd.Data));
                    }
                    catch { }
                }

                System.Diagnostics.Debug.WriteLine($"loop6: per={persisting.GetHashCode()}");

                ProcessCommandInfo[] removedCMDs;
                lock (sendCMDs)
                {
                    IEnumerable<string> cmdIDs = cmds.Select(cmd => cmd.ID);
                    removedCMDs = sendCMDs.Where(cmd => cmd.Name == receivedCmdName && !cmdIDs.Contains(cmd.Data)).ToArray();
                }
                foreach (ProcessCommandInfo cmd in removedCMDs)
                {
                    System.Diagnostics.Debug.WriteLine($"remove received cmd: per={persisting.GetHashCode()} {ToString(cmd)}");
                    RemoveCommand(cmd.ID);
                    changedCommands = true;
                }

                System.Diagnostics.Debug.WriteLine($"loop9: per={persisting.GetHashCode()} changedCommands={changedCommands} semLoop={semLoop.CurrentCount}");
                if (changedCommands) await WriteCommands();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"loop error: per={persisting.GetHashCode()} e={e}");
            }
            finally
            {
                semLoop.Release(1);
                //System.Diagnostics.Debug.WriteLine($"loop10 per={persisting.GetHashCode()} semLoop={semLoop.CurrentCount}");
            }
        }

        protected abstract void ReceiveCommand(ReceivedProcessCommand cmd);

        protected void StartTimer()
        {
            System.Diagnostics.Debug.WriteLine($"StartTimer per={persisting.GetHashCode()} now={DateTime.Now.TimeOfDay}");
            timer.Change(0, 100);
        }

        protected void StopTimer()
        {
            timer.Change(Timeout.Infinite, 100);
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

        protected async void SendDataCommand(string name, string data, string key = null)
        {
            AppendCommand(name, data, key);
            await WriteCommands();
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}
