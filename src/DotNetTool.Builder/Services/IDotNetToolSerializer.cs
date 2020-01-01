namespace DotNetTool.Builder.Services
{
    using FileSystemAbstraction;
    using Models;

    public interface IDotNetToolSerializer
    {
        DotNetTool DeserializeFrom(IFileInfo fileInfo);
        DotNetTool DeserializeFrom(string fileOrFilePath);
    }
}