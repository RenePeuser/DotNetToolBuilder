using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Builder.Startup
{
    public interface IStartUpBuilder
    {
        void AddRegistrationsFrom(string projectName, IFileInfo solutionFile,
            ICommandTypeCollector commandTypeCollector, CommandInfo rootCommand,
            INameSpaceCollector nameSpaceCollector);
    }
}
