using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(Models.DotNetTool dotNetTool);
    }
}
