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
    internal class JetBrainsRider : ISpecificIDE
    {
        private readonly IDirectoryService _directoryService;
        private readonly IConsoleService _consoleService;
        private readonly IProcessService _processService;

        public JetBrainsRider(IProcessService processService, IDirectoryService directoryService, IConsoleService consoleService)
        {
            _processService = processService;
            _directoryService = directoryService;
            _consoleService = consoleService;
        }

        public Task OpenAsync(IFileInfo solution, NewToolParameters parameters)
        {
            Throw.IfNull(() => solution);
            Throw.IfNull(() => parameters);

            if (parameters.UseRider.IsFalse())
            {
                return Task.CompletedTask;
            }

            // C:\Program Files\JetBrains\JetBrains Rider 2019.3.1\bin\rider64.exe
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var jetbrainsFolder = _directoryService.GetDirectoryInfo(Path.Combine(programx86Path, "JetBrains"));
            if (jetbrainsFolder.NotExists)
            {
                _consoleService.WriteError($"JetBrains Rider IDE could not be started.{Environment.NewLine}Could not find any installation of 'JetBrains Rider' in folder: '{jetbrainsFolder.FullName}'{Environment.NewLine}");
                return Task.CompletedTask;
            }

            var riderDirectory = jetbrainsFolder.EnumerateDirectories().FirstOrDefault(directory => directory.Name.Contains("Rider"));
            if (riderDirectory.IsNull())
            {
                _consoleService.WriteError($"Can not find any installation of 'JetBrains Rider' in folder: '{jetbrainsFolder.FullName}'");
                return Task.CompletedTask;
            }

            var riderExecutable = riderDirectory.EnumerateFiles("rider*.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (riderExecutable.IsNull())
            {
                _consoleService.WriteError($"Can not find 'rider*.exe: ' in directory and its sub directories: {jetbrainsFolder.FullName}'");
                return Task.CompletedTask;
            }

            _consoleService.WriteInfo($"Start {riderDirectory.Name} with {solution.Name}");
            return _processService.StartCliCommandAsync(riderExecutable.FullName, solution.FullName);
        }
    }
}