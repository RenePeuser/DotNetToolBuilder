using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.Startup
{
    internal interface IStartUpBuilder
    {
        void AddRegistrationsFrom(string projectName, IFileInfo solutionFile,
            ICommandTypeCollector commandTypeCollector, CommandInfo rootCommand,
            INameSpaceCollector nameSpaceCollector);
    }
}
