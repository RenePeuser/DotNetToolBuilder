using FileSystem.Abstraction;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.Services.DotNet
{
    internal interface IDotNetToolSerializer
    {
        Models.DotNetTool DeserializeFrom(IFileInfo fileInfo);
        Models.DotNetTool DeserializeFrom(string fileOrFilePath);
        Models.DotNetTool DeserializeFrom(FileInfo fileInfo);
    }
}