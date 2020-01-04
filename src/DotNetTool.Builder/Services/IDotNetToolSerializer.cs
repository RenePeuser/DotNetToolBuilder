using FileSystem.Abstraction;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.Services
{
    using Models;

    internal interface IDotNetToolSerializer
    {
        DotNetTool DeserializeFrom(IFileInfo fileInfo);
        DotNetTool DeserializeFrom(string fileOrFilePath);
        DotNetTool DeserializeFrom(FileInfo fileInfo);
    }
}