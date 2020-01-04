using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    internal interface IDotNetToolSerializer
    {
        DotNetTool DeserializeFrom(IFileInfo fileInfo);
        DotNetTool DeserializeFrom(string fileOrFilePath);
    }
}