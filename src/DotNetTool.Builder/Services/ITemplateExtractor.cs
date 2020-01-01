using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public interface ITemplateExtractor
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}