using System.Threading.Tasks;

namespace DotNetTool.Builder.Services
{
    public interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
    }
}