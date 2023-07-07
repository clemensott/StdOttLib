using System.Threading.Tasks;

namespace StdOttStandard.ProcessCommunication
{
    public interface IProcessComandPersisting
    {
        Task<ProcessCommandInfo[]> ReadCommands();

        Task WriteCommands(ProcessCommandInfo[] cmds);
    }
}
