using System.Threading.Tasks;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IDotNetToolService
    {
        Task<CliRunResult> InstallAsync(string toolName, string version);

        Task<DotNetToolInfo?> ExistsAsync(string toolName);

        Task<CliRunResult> UninstallAsync(string toolName);
    }
}