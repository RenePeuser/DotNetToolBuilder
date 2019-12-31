using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface ICreateCommandClasses
    {
        void Invoke(string projectName, CommandInfo parameter, IDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector);
    }
}
