using System.Threading.Tasks;

namespace StdOttStandard.ProcessCommunication
{
    public abstract class ProcessCommandXmlPersisting : IProcessComandPersisting
    {
        private string lastReadXML = null;

        public async Task<ProcessCommandInfo[]> ReadCommands()
        {
            string xml = await ReadCommandsXML();
            try
            {
                if (string.IsNullOrWhiteSpace(xml) || xml == lastReadXML) return null;
                return StdUtils.XmlDeserializeText<ProcessCommandInfo[]>(xml);
            }
            finally
            {
                lastReadXML = xml;
            }
        }

        protected abstract Task<string> ReadCommandsXML();

        public Task WriteCommands(ProcessCommandInfo[] cmds)
        {
            string xml = StdUtils.XmlSerialize(cmds);
            return WriteCommandsXML(xml);
        }

        protected abstract Task WriteCommandsXML(string xml);
    }
}
