using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal interface IBuildCommandFileStructure
    {
        void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand);
    }
}
