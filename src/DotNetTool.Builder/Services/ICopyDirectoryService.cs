
using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public interface ICopyDirectoryService
    {
        void CopyDirectory(IDirectoryInfo sourceDirectory, IDirectoryInfo targetDirectory);
        void CopyDirectory(IDirectoryInfo sourceDirectory, IDirectoryInfo targetDirectory, bool copySubDirs);
    }
}