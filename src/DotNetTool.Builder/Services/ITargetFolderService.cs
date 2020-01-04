using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    public interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(DotNetTool dotNetTool);
    }
}