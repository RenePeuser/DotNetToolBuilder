using System.IO;
using DotNetTool.Builder.ArgumentChecking;

namespace DotNetTool.Builder.Extensions
{
    public static class FileSystemInfoExtensions
    {
        public static bool NotExists(this FileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return !fileSystemInfo.Exists;
        }

        public static string FileNameWithoutExtension(this FileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return fileSystemInfo.Name.Replace(fileSystemInfo.Extension, string.Empty);
        }
    }
}
