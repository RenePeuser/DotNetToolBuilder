using System;
using System.IO;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Dotnet.Newtool;

using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal class VisualStudioCode : ISpecificIDE
    {
        private readonly IConsoleService _consoleService;
        private readonly IFileService _fileService;
        private readonly IProcessService _processService;

        public VisualStudioCode(IProcessService processService, IFileService fileService, IConsoleService consoleService)
        {
            _processService = processService;
            _fileService = fileService;
            _consoleService = consoleService;
        }

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
            var codeInLocalAppData = _fileService.GetFileInfo(Path.Combine(localAppData, "Programs", "Microsoft VS Code", "code.exe"));
            if (codeInLocalAppData.Exists)
            {
                _consoleService.WriteInfo($"Start Visual Studio Code: '{solutionFileInfo.Directory.Parent.FullName}'");
                return _processService.StartAsync(codeInLocalAppData.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            // C:/Program Files/Microsoft VS Code/Code.exe
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var codeInProgramFolder = _fileService.GetFileInfo(Path.Combine(programFiles, "Microsoft VS Code", "code.exe"));
            if (codeInProgramFolder.Exists)
            {
                _consoleService.WriteInfo($"Start Visual Studio Code: '{solutionFileInfo.Directory.Parent.FullName}'");
                return _processService.StartAsync(codeInProgramFolder.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            _consoleService.WriteError($"Could not detect an installation path of visual studio code. Looked in:{Environment.NewLine}- {codeInLocalAppData.FullName}{Environment.NewLine}- {codeInProgramFolder.FullName}");
            return Task.CompletedTask;
        }
    }
}
