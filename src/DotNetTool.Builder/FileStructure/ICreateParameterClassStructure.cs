using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal interface ICreateParameterClassStructure
    {
        void Create(string projectName, string currentPath, INameSpaceCollector namespaceCollector, CommandInfo subCommand, IDirectoryInfo subCommnandDirectoryInfo);
    }
}