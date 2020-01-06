using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface ITypeService
    {
        string GetFullqualifiedName(string projectName, IFileInfo fileInfo);
    }
}
