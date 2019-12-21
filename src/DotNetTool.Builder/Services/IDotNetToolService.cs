using System.Threading.Tasks;

namespace DotNetTool.Builder.Services
{
    public interface IDotNetToolService
    {
        Task<CliRunResult> InstallAsync(string toolName, string version);

        Task<DotNetToolInfo?> ExistsAsync(string toolName);

        Task<CliRunResult> UninstallAsync(string toolName);
    }
}