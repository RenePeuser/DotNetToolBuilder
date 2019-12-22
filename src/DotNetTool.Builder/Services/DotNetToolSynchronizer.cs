using System;
using System.Threading.Tasks;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Services
{
    public class DotNetToolSynchronizer : IDotNetToolSynchronizer
    {
        private readonly IConsoleService _consoleService;
        private readonly IDotNetToolService _dotNetToolService;

        public DotNetToolSynchronizer(IDotNetToolService dotNetToolService, IConsoleService consoleService)
        {
            _dotNetToolService = dotNetToolService;
            _consoleService = consoleService;
        }

        public async Task SynchronizeTool(DotNetToolInfo dotNetToolInfo)
        {
            // This is only temporary until the real uif packer is integrated here.
            var tool = await _dotNetToolService.ExistsAsync(dotNetToolInfo.Name);
            if (tool != null)
            {
                if (tool.Version.NotEqualsTo(dotNetToolInfo.Version))
                {
                    await UninstallTool(dotNetToolInfo);
                    await InstallTool(dotNetToolInfo);
                }
            }
            else
            {
                await InstallTool(dotNetToolInfo);
            }
        }

        private async Task UninstallTool(DotNetToolInfo dotNetToolInfo)
        {
            var unistallResult = await _dotNetToolService.UninstallAsync(dotNetToolInfo.Name);
            if (unistallResult.ExitCode != 0) throw new Exception(unistallResult.Output);
        }

        private async Task InstallTool(DotNetToolInfo dotNetToolInfo)
        {
            _consoleService.WriteLine($"Install '{dotNetToolInfo.Name}'");
            var result = await _dotNetToolService.InstallAsync(dotNetToolInfo.Name, dotNetToolInfo.Version);
            if (result.ExitCode != 0) throw new Exception(result.Output);
        }
    }
}