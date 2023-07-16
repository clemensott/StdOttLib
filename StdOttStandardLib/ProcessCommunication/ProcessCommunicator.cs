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
        public const string PingCmdName = "ping";

        private bool isRunning, isPingEnabled, isForwardingPings;
        protected readonly IProcessComandPersisting persisting;
        private readonly int maxCmdCount;
        private readonly List<ProcessCommandInfo> sendCMDs;
        private readonly Dictionary<string, string> keysDict;
        private readonly HashSet<string> receivedCMDs;
        private readonly Timer writeTime, readTimer, pingTimer;
        private readonly SemaphoreSlim semWriteLoop, semReadLoop;
        private string[] lastWriteCommandIds;

        public bool IsPingEnabled
        {
            get => isPingEnabled;
            set
            {
                if (value == isPingEnabled) return;

                isPingEnabled = value;
                UpdatePingTimer();
            }
        }

        public bool IsForwardingPings
        {
            get => isForwardingPings;
            set => isForwardingPings = value;
        }

        public ProcessCommunicator(IProcessComandPersisting persisting, int maxCmdCount = 100)
        {
            this.persisting = persisting;
            this.maxCmdCount = maxCmdCount;

            sendCMDs = new List<ProcessCommandInfo>();
            keysDict = new Dictionary<string, string>();
            receivedCMDs = new HashSet<string>();
            writeTime = new Timer(new TimerCallback(WriteLoop), null, Timeout.Infinite, 1);
            readTimer = new Timer(new TimerCallback(ReadLoop), null, Timeout.Infinite, 1);
            pingTimer = new Timer(new TimerCallback(ReadLoop), null, Timeout.Infinite, 1);
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
            lock (sendCMDs)
            {
                if (key != null)
                {
                    string keyCmdId;
                    if (keysDict.TryGetValue(key, out keyCmdId))
                    {
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
                    writeCMDs = (maxCmdCount > 0 ? sendCMDs.Take(maxCmdCount) : sendCMDs).ToArray();
                }

                string[] writeCmdIds = writeCMDs.Select(c => c.ID).ToArray();
                if (!writeCmdIds.SequenceEqual(lastWriteCommandIds))
                {
                    await persisting.WriteCommands(writeCMDs.ToArray());
                    lastWriteCommandIds = writeCmdIds;
                }

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
            if (semReadLoop.CurrentCount == 0) return;
            await semReadLoop.WaitAsync();
            try
            {
                ProcessCommandInfo[] cmds = await persisting.ReadCommands();
                if (cmds == null) return;

                List<ReceivedProcessCommand> receiveCmds = new List<ReceivedProcessCommand>();
                foreach (ProcessCommandInfo cmd in cmds)
                {
                    if (receivedCMDs.Contains(cmd.ID)) continue;

                    receivedCMDs.Add(cmd.ID);

                    if (cmd.Name == receivedCmdName)
                    {
                        RemoveCommandById(cmd.Data);
                        continue;
                    }

                    AppendCommand(receivedCmdName, cmd.ID, null);
                    try
                    {
                        if (cmd.Name != PingCmdName || IsForwardingPings)
                        {
                            ReceivedProcessCommand receiveCmd = new ReceivedProcessCommand(cmd.Name, cmd.Data);
                            receiveCmds.Add(receiveCmd);
                            ReceiveCommand(receiveCmd);
                        }
                    }
                    catch { }
                }

                try
                {
                    if (receiveCmds.Count > 0) ReceiveCommands(receiveCmds);
                }
                catch { }

                ProcessCommandInfo[] removedCMDs;
                lock (sendCMDs)
                {
                    IEnumerable<string> cmdIDs = cmds.Select(cmd => cmd.ID);
                    removedCMDs = sendCMDs.Where(cmd => cmd.Name == receivedCmdName && !cmdIDs.Contains(cmd.Data)).ToArray();
                }
                foreach (ProcessCommandInfo cmd in removedCMDs)
                {
                    RemoveCommandById(cmd.ID);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"read loop error: per={persisting.GetHashCode()} e={e}");
            }
            finally
            {
                semReadLoop.Release(1);
            }
        }

        private void PingLoop(object state)
        {
            SendKeyCommand(PingCmdName);
        }

        protected virtual void ReceiveCommand(ReceivedProcessCommand cmd) { }

        protected virtual void ReceiveCommands(ICollection<ReceivedProcessCommand> cmds) { }

        protected void StartTimer()
        {
            if (isRunning) return;

            isRunning = true;
            writeTime.Change(0, 100);
            readTimer.Change(0, 100);
            UpdatePingTimer();
        }

        protected void StopTimer()
        {
            if (!isRunning) return;

            isRunning = false;
            writeTime.Change(Timeout.Infinite, 100);
            readTimer.Change(Timeout.Infinite, 100);
            UpdatePingTimer();
        }

        private void UpdatePingTimer()
        {
            if (isRunning && IsPingEnabled) pingTimer.Change(0, 500);
            else pingTimer.Change(Timeout.Infinite, 100);
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

        public void Dispose()
        {
            writeTime.Dispose();
            readTimer.Dispose();
            pingTimer.Dispose();
        }
    }
}
