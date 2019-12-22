using System;
using System.IO;

namespace DotNetTool.Builder.FileSystemAbstraction
{
    public interface IFileSystemInfo
    {
        FileAttributes Attributes { get; set; }

        DateTime CreationTime { get; set; }

        bool Exists { get; }

        string Extension { get; }

        string FullName { get; }

        DateTime LastAccessTime { get; set; }

        DateTime LastWriteTime { get; set; }

        string Name { get; }

        void Delete();

        void Refresh();
    }
}
