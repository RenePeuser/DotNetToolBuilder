using System.Threading.Tasks;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services.Process
{
    internal interface IProcessService
    {
        Task<CliRunResult> RunCliCommandAsync(string command, string arguments);
        Task<CliRunResult> StartCliCommandAsync(string command, string arguments);
    }
}
