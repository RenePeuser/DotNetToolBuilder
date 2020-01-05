using Argument.Check;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Extensions
{
    internal static class IFileSystemInfoExtensions
    {
        internal static bool NotExists(this IFileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return !fileSystemInfo.Exists;
        }

        internal static string FileNameWithoutExtension(this IFileSystemInfo fileSystemInfo)
        {
            Throw.IfNull(() => fileSystemInfo);

            return fileSystemInfo.Name.Replace(fileSystemInfo.Extension, string.Empty);
        }
    }
}
