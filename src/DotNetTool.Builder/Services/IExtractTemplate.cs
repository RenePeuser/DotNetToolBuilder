using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public interface IExtractTemplate
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}