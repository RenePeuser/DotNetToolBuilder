using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    public interface ITypeService
    {
        string GetFullqualifiedName(string projectName, IFileInfo fileInfo);
    }
}