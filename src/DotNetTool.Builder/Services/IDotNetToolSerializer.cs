using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    public interface IDotNetToolSerializer
    {
        DotNetTool DeserializeFrom(IFileInfo fileInfo);
        DotNetTool DeserializeFrom(string fileOrFilePath);
    }
}