using System.Threading.Tasks;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
    }
}
