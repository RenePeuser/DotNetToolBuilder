using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    public interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, DotNetTool dotNetTool);
    }
}