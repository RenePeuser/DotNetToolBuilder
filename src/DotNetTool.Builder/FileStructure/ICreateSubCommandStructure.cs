using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal interface ICreateSubCommandStructure
    {
        void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, CommandInfo subCommand, IDirectoryInfo subCommnandDirectoryInfo);
    }
}