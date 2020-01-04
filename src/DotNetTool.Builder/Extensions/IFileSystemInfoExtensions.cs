using Argument.Check;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Extensions
{
    public static class IFileSystemInfoExtensions
    {
        public static bool NotExists(this IFileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return !fileSystemInfo.Exists;
        }

        public static string FileNameWithoutExtension(this IFileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return fileSystemInfo.Name.Replace(fileSystemInfo.Extension, string.Empty);
        }
    }
}
