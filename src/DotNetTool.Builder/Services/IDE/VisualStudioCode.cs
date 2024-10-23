using System;
using System.IO;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal sealed class VisualStudioCode(IProcessService processService,
                                           IFileService fileService,
                                           IConsoleService consoleService) : ISpecificIDE
    {
        public Task OpenAsync(IFileInfo solutionFileInfo, NewToolParameters parameters)
        {
            Throw.IfNull(() => solutionFileInfo);
            Throw.IfNull(() => parameters);

            if (parameters.UseVsCode.IsFalse())
            {
                return Task.CompletedTask;
            }

            // C:/users/{username}/AppData/Local/Programs/Microsoft VS Code
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var codeInLocalAppData = fileService.GetFileInfo(Path.Combine(localAppData, "Programs", "Microsoft VS Code", "code.exe"));
            if (codeInLocalAppData.Exists)
            {
                consoleService.WriteInfo($"Start Visual Studio Code: '{solutionFileInfo.Directory.Parent.FullName}'");
                return processService.StartAsync(codeInLocalAppData.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            // C:/Program Files/Microsoft VS Code/Code.exe
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var codeInProgramFolder = fileService.GetFileInfo(Path.Combine(programFiles, "Microsoft VS Code", "code.exe"));
            if (codeInProgramFolder.Exists)
            {
                consoleService.WriteInfo($"Start Visual Studio Code: '{solutionFileInfo.Directory.Parent.FullName}'");
                return processService.StartAsync(codeInProgramFolder.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            consoleService.WriteError($"Could not detect an installation path of visual studio code. Looked in:{Environment.NewLine}- {codeInLocalAppData.FullName}{Environment.NewLine}- {codeInProgramFolder.FullName}");
            return Task.CompletedTask;
        }
    }
}
