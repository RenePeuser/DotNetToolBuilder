using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IDE
{
    internal sealed class JetBrainsRider(IProcessService processService,
                                         IDirectoryService directoryService,
                                         IConsoleService consoleService) : ISpecificIDE
    {
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
            var jetbrainsFolder = directoryService.GetDirectoryInfo(Path.Combine(programx86Path, "JetBrains"));
            if (jetbrainsFolder.NotExists)
            {
                consoleService.WriteError($"JetBrains Rider IDE could not be started.{Environment.NewLine}Could not find any installation of 'JetBrains Rider' in folder: '{jetbrainsFolder.FullName}'{Environment.NewLine}");
                return Task.CompletedTask;
            }

            var riderDirectory = jetbrainsFolder.EnumerateDirectories().FirstOrDefault(directory => directory.Name.Contains("Rider"));
            if (riderDirectory.IsNull())
            {
                consoleService.WriteError($"Can not find any installation of 'JetBrains Rider' in folder: '{jetbrainsFolder.FullName}'");
                return Task.CompletedTask;
            }

            var riderExecutable = riderDirectory.EnumerateFiles("rider*.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (riderExecutable.IsNull())
            {
                consoleService.WriteError($"Can not find 'rider*.exe: ' in directory and its sub directories: '{jetbrainsFolder.FullName}'");
                return Task.CompletedTask;
            }

            consoleService.WriteInfo($"Start {riderDirectory.Name} with {solution.Name}");
            return processService.StartAsync(riderExecutable.FullName, solution.FullName);
        }
    }
}
