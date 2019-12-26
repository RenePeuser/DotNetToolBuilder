using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.App
{
    public interface IExtractTemplate
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}