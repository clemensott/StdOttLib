using System;
using System.Threading.Tasks;
using StdOttStandard.ProcessCommunication;
using Windows.Storage;

namespace StdOttUwp.ProcessCommunication
{
    public class FileProcessCmdPersisting : ProcessCommandXmlPersisting
    {
        private readonly StorageFolder folder;
        private readonly string readCmdsFileName, writeCmdsFileName;
        private StorageFile readFile, writeFile;

        public FileProcessCmdPersisting(StorageFolder folder, string readCmdsFileName, string writeCmdsFileName)
        {
            this.folder = folder;
            this.readCmdsFileName = readCmdsFileName;
            this.writeCmdsFileName = writeCmdsFileName;
        }

        protected override async Task<string> ReadCommandsXML()
        {
            if (readFile == null)
            {
                readFile = await folder.TryGetFileAsync(readCmdsFileName);
                if (readFile == null) return null;
            }
            return await FileIO.ReadTextAsync(readFile);
        }

        protected override async Task WriteCommandsXML(string xml)
        {
            if (writeFile == null)
            {
                writeFile = await folder.CreateFileAsync(writeCmdsFileName, CreationCollisionOption.OpenIfExists);
            }
            await FileIO.WriteTextAsync(writeFile, xml);
        }
    }
}
