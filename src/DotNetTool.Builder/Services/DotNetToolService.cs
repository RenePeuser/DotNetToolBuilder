using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.ArgumentChecking;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Services
{
    public class DotNetToolService : IDotNetToolService
    {
        private readonly IProcessService _processService;

        public DotNetToolService(IProcessService processService)
        {
            _processService = processService;
        }

        public Task<CliRunResult> InstallAsync(string toolName, string version)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);
            Throw.IfNullOrWhiteSpace(() => version);

            return _processService.RunCliCommandAsync("dotnet", $"tool install -g {toolName} --version {version}");
        }

        public async Task<DotNetToolInfo?> ExistsAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            // Package Id                              Version      Commands
            // ------------------------------------------------------------------------
            // gitversion.tool                         5.0.1        dotnet-gitversion
            // trumpf.uifpack                          3.0.0        uif-pack
            var listResult = await _processService.RunCliCommandAsync("dotnet", "tool list -g");
            if (listResult.ExitCode != 0) throw new Exception($"Error occured: '{listResult.Output}'");

            var toolRows = listResult.Output.Split("\r\n");
            var tool = toolRows.FirstOrDefault(line => line.Contains(toolName.ToLower()));
            if (tool.IsNull()) return null;

            var toolInfos = tool.Split(" ").Where(s => s.IsNotNullOrWhiteSpace()).ToList();
            return new DotNetToolInfo(toolInfos[0], toolInfos[1], toolInfos[2]);
        }

        public Task<CliRunResult> UninstallAsync(string toolName)
        {
            Throw.IfNullOrWhiteSpace(() => toolName);

            return _processService.RunCliCommandAsync("dotnet", $"tool uninstall -g {toolName}");
        }
    }
}