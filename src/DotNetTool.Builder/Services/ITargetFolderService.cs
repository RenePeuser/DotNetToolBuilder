using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    internal interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(DotNetTool dotNetTool);
    }
}