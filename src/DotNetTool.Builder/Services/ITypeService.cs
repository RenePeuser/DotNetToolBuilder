using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface ITypeService
    {
        string GetFullQualifiedName(string projectName, IFileInfo fileInfo);
    }
}
