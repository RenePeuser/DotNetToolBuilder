using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface ICreateCommandClasses
    {
        void Invoke(string projectName, ParameterInfo parameter, IDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector);
    }
}
