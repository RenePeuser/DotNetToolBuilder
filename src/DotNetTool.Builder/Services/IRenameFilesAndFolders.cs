using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface IRenameFilesAndFolders
    {
        void Rename(IDirectoryInfo directoryInfo, string originalName, string newName);
    }
}
