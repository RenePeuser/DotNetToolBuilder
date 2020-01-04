
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    public interface ITemplateExtractor
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}