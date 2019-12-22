namespace DotNetTool.Builder.FileSystemAbstraction.Services
{
    public interface IFileService
    {
        IFileInfo GetFileInfo(string path);
    }
}
