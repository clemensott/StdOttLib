using StdOttStandard.ProcessCommunication;
using System.Threading.Tasks;
using Windows.Storage;

namespace StdOttUwp.ProcessCommunication
{
    public class AppDataContainerProcessCmdPersisting : ProcessCommandXmlPersisting
    {
        private readonly ApplicationDataContainer dataContainer;
        private readonly string readCmdsName, writeCmdsName;

        public AppDataContainerProcessCmdPersisting(ApplicationDataContainer dataContainer, string readCmdsName, string writeCmdsName)
        {
            this.dataContainer = dataContainer;
            this.readCmdsName = readCmdsName;
            this.writeCmdsName = writeCmdsName;
        }

        protected override Task<string> ReadCommandsXML()
        {
            string result = null;
            object obj;

            if (dataContainer.Values.TryGetValue(readCmdsName, out obj)) result = obj as string;

            return Task.FromResult(result);
        }

        protected override Task WriteCommandsXML(string xml)
        {
            dataContainer.Values[writeCmdsName] = xml;
            return Task.CompletedTask;
        }
    }
}
