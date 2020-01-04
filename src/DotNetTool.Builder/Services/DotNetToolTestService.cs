using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Argument.Check;
    using Models;

    public class DotNetToolTestService : IDotNetToolTestService
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

        public async Task RunAsync(IFileInfo solutionFile, DotNetTool dotNetTool)
        {
            var findExe = solutionFile.Directory.EnumerateFiles($"{dotNetTool.ProjectName}.exe", SearchOption.AllDirectories).FirstOrDefault();

            _consoleService.WriteInfo($"Test run of your: '{dotNetTool.ProjectName}' dotnet tool");

            var runYourCliResult = await _processService.RunCliCommandAsync($"{findExe.FullName}", "--help");
        }
    }
}