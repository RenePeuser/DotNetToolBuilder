
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface ITemplateExtractor
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}