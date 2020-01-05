using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services.Process;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal class VisualStudioCode : ISpecificIDE
    {
        private readonly IFileService _fileService;
        private readonly IConsoleService _consoleService;
        private readonly IProcessService _processService;

        public VisualStudioCode(IProcessService processService, IFileService fileService, IConsoleService consoleService)
        {
            _processService = processService;
            _fileService = fileService;
            _consoleService = consoleService;
        }

        public Task OpenAsync(IFileInfo solutionFileInfo, NewToolParameters parameters)
        {
            if (parameters.UseVsCode.IsFalse())
            {
                return Task.CompletedTask;
            }

            // C:/users/{username}/AppData/Local/Programs/Microsoft VS Code
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var codeInLocalAppData = _fileService.GetFileInfo(Path.Combine(localAppData, "Programs", "Microsoft VS Code", "code.exe"));
            if (codeInLocalAppData.Exists)
            {
                _consoleService.WriteInfo($"Start Visual Studio Code: {solutionFileInfo.Directory.Parent.FullName}");
                return _processService.StartCliCommandAsync(codeInLocalAppData.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            // C:/Program Files/Microsoft VS Code/Code.exe
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var codeInProgramFolder = _fileService.GetFileInfo(Path.Combine(programFiles, "Microsoft VS Code", "code.exe"));
            if (codeInProgramFolder.Exists)
            {
                _consoleService.WriteInfo($"Start Visual Studio Code: {solutionFileInfo.Directory.Parent.FullName}");
                return _processService.StartCliCommandAsync(codeInProgramFolder.FullName, solutionFileInfo.Directory.Parent.FullName);
            }

            _consoleService.WriteError($"Could not detect installation path of visual studio code. Looked in:{Environment.NewLine}- {codeInLocalAppData.FullName}{Environment.NewLine}- {codeInProgramFolder}");

            return Task.CompletedTask;
        }
    }
}