using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal interface ICreateCommandClasses
    {
        void Invoke(string projectName, CommandInfo parameter, IDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector);
    }
}
