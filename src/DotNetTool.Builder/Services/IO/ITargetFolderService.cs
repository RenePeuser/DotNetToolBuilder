using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IO
{
    internal interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(Models.DotNetTool dotNetTool);
    }
}
