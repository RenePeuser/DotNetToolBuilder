using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Services.Process;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.DotNet
{
    internal class DotNetToolTestService : IDotNetToolTestService
    {
        private readonly IConsoleService _consoleService;
        private readonly IProcessService _processService;

        public DotNetToolTestService(IConsoleService consoleService, IProcessService processService)
        {
            Throw.IfNull(() => consoleService);
            Throw.IfNull(() => processService);

            _consoleService = consoleService;
            _processService = processService;
        }

        public Task RunAsync(IFileInfo solutionFile, Models.DotNetTool dotNetTool)
        {
            var fileExtension = Environment.OSVersion.VersionString.Contains("windows") ? "exe" : "dll";
            var findExe = solutionFile.Directory.EnumerateFiles($"{dotNetTool.ProjectName}.{fileExtension}", SearchOption.AllDirectories).FirstOrDefault();

            _consoleService.WriteInfo($"Test run of your: '{dotNetTool.ProjectName}' dotnet tool");

            return _processService.RunCliCommandAsync($"{findExe.FullName}", "--help");
        }
    }
}
