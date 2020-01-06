using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IO
{
    internal interface IRenameFilesAndFolders
    {
        void Rename(IDirectoryInfo directoryInfo, string originalName, string newName);
    }
}
