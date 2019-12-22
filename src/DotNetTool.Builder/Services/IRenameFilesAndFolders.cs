using System.IO;
using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Services
{
    public interface IRenameFilesAndFolders
    {
        void Rename(TiDirectoryInfo directoryInfo, string originalName, string newName);
    }
}