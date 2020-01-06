using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IO
{
    internal interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(Models.DotNetTool dotNetTool);
        IDirectoryInfo GetToolFolder(Models.DotNetTool dotNetTool, IDirectoryInfo targetDirectoryInfo);
        IFileInfo GetSolutionFile(Models.DotNetTool dotNetTool, IDirectoryInfo targetDirectoryInfo);
    }
}
