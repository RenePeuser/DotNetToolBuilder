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
    internal sealed class VisualStudio(IProcessService processService,
                                       IDirectoryService directoryService,
                                       IConsoleService consoleService) : ISpecificIDE
    {
        public Task OpenAsync(IFileInfo solution, NewToolParameters parameters)
        {
            Throw.IfNull(() => solution);
            Throw.IfNull(() => parameters);

            if (parameters.UseVisualStudio.IsFalse())
            {
                return Task.CompletedTask;
            }

            // root:\Program Files (x86)\Microsoft Visual Studio\2017
            // root:\Program Files (x86)\Microsoft Visual Studio\2019
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

            var visualStudioFolder = directoryService.GetDirectoryInfo(Path.Combine(programx86Path, @"Microsoft Visual Studio\"));
            if (visualStudioFolder.NotExists)
            {
                consoleService.WriteError($"Visual studio folder: '{visualStudioFolder.FullName}' does not exists.{Environment.NewLine}Can not start any install Visual Studio version.");
                return Task.CompletedTask;
            }

            var lastVisualStudioVersion = visualStudioFolder.EnumerateDirectories("20*").OrderBy(d => d.Name).LastOrDefault();
            if (lastVisualStudioVersion.IsNull())
            {
                consoleService.WriteError($"No visual studio version folder found in: '{visualStudioFolder.FullName}'.{Environment.NewLine}Can not start any Version of Visual Studio.");
                return Task.CompletedTask;
            }

            if (lastVisualStudioVersion.NotExists)
            {
                consoleService.WriteError($"Visual Studio folder: '{lastVisualStudioVersion.FullName}' does not exists.{Environment.NewLine}Can not start any Versin of Visual Studio.");
                return Task.CompletedTask;
            }

            var latest = lastVisualStudioVersion.EnumerateFiles("devenv.exe", SearchOption.AllDirectories).FirstOrDefault();
            if (latest.NotExists)
            {
                consoleService.WriteError($"Could not start Visual Studio {lastVisualStudioVersion.NotExists}.{Environment.NewLine}Could not found: 'devenv.exe' in VS2019 folder: '{lastVisualStudioVersion.FullName}'");
                return Task.CompletedTask;
            }

            consoleService.WriteInfo($"Start Visual Studio {lastVisualStudioVersion.NotExists} with: '{solution.Name}'");
            return processService.StartAsync(latest.FullName, solution.FullName);
        }
    }
}
