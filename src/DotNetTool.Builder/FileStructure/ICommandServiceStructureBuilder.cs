using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal interface ICommandServiceStructureBuilder
    {
        void Create(string projectName, ICommandTypeCollector commandTypeCollector, string currentPath, CommandInfo subCommand, IDirectoryInfo subCommnandDirectoryInfo);
    }
}