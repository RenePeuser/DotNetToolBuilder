using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

namespace DotNetTool.Builder.Extensions
{
    public static class TcFileSystemInfoExtensions
    {
        public static bool NotExists(this TiFileSystemInfo fileSystemInfo)
        {
            return !fileSystemInfo.Exists;
        }

        public static string FileNameWithoutExtension(this TiFileSystemInfo fileSystemInfo)
        {
            return fileSystemInfo.Name.Replace(fileSystemInfo.Extension, string.Empty);
        }
    }
}
