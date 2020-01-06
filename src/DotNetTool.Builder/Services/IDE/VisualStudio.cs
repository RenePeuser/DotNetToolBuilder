using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services.Process;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal class VisualStudio : ISpecificIDE
    {
        private readonly IConsoleService _consoleService;
        private readonly IDirectoryService _directoryService;
        private readonly IProcessService _processService;

        public VisualStudio(IProcessService processService, IDirectoryService directoryService, IConsoleService consoleService)
        {
            _processService = processService;
            _directoryService = directoryService;
            _consoleService = consoleService;
        }

        public Task OpenAsync(IFileInfo solution, NewToolParameters parameters)
        {
            Throw.IfNull(() => solution);
            Throw.IfNull(() => parameters);

            if (parameters.UseVisualStudio.IsFalse())
            {
                return Task.CompletedTask;
            }

            // C:\Program Files (x86)\Microsoft Visual Studio\2019
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var visualStudio2019Folder = _directoryService.GetDirectoryInfo(Path.Combine(programx86Path, @"Microsoft Visual Studio\2019\"));
            if (visualStudio2019Folder.NotExists)
            {
                _consoleService.WriteError($"VS2019 Folder: '{visualStudio2019Folder.FullName}' does not exists.{Environment.NewLine}Can not start VS2019");
                return Task.CompletedTask;
            }

            var vs2019 = visualStudio2019Folder.EnumerateFiles("devenv.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (vs2019.NotExists())
            {
                _consoleService.WriteError($"Could not start VS2019.{Environment.NewLine}Could not found: 'devenv.exe' in VS2019 folder: '{visualStudio2019Folder.FullName}'");
                return Task.CompletedTask;
            }

            _consoleService.WriteInfo($"Start Visual Studio 2019 with: {solution.Name}");
            return _processService.StartCliCommandAsync(vs2019.FullName, solution.FullName);
        }
    }
}
