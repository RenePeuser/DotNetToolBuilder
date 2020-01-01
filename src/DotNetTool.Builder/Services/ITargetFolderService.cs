namespace DotNetTool.Builder.Services
{
    using FileSystemAbstraction;
    using Models;

    public interface ITargetFolderService
    {
        IDirectoryInfo CreateTargetDirectory(DotNetTool dotNetTool);
    }
}