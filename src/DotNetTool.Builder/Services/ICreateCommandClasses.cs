using DotNetTool.Builder.Models;
using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Services
{
    internal interface ICreateCommandClasses
    {
        void Invoke(string projectName, ParameterInfo parameter, TiDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector);
    }
}