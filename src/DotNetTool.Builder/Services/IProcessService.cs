using System.Threading.Tasks;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface IProcessService
    {
        Task<CliRunResult> RunAsync(string command, string arguments);
        Task<CliRunResult> StartAsync(string command, string arguments);
    }
}
