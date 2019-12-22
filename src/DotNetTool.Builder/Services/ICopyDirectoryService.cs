using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Services
{
    public interface ICopyDirectoryService
    {
        void CopyDirectory(TiDirectoryInfo sourceDirectory, TiDirectoryInfo targetDirectory);
        void CopyDirectory(TiDirectoryInfo sourceDirectory, TiDirectoryInfo targetDirectory, bool copySubDirs);
    }
}