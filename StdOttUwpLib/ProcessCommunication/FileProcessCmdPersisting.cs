using System;
using System.Threading.Tasks;
using StdOttStandard.ProcessCommunication;
using Windows.Storage;

namespace StdOttUwp.ProcessCommunication
{
    public class FileProcessCmdPersisting : ProcessCommandXmlPersisting
    {
        private readonly StorageFolder folder;
        private readonly string readCmdsFileName, writeCmdsFileName, writeTmpCmdsFileName;
        private StorageFile readFile;

        public FileProcessCmdPersisting(StorageFolder folder, string readCmdsFileName, string writeCmdsFileName, string writeTmpCmdsFileName)
        {
            this.folder = folder;
            this.readCmdsFileName = readCmdsFileName;
            this.writeCmdsFileName = writeCmdsFileName;
            this.writeTmpCmdsFileName = writeTmpCmdsFileName;
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
            StorageFile writeTmpFile = await folder.CreateFileAsync(writeTmpCmdsFileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(writeTmpFile, xml);
            await writeTmpFile.MoveAsync(folder, writeCmdsFileName, NameCollisionOption.ReplaceExisting);
        }
    }
}
