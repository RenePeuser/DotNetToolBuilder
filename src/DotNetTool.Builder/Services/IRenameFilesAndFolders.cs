using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public interface IRenameFilesAndFolders
    {
        void Rename(IDirectoryInfo directoryInfo, string originalName, string newName);
    }
}