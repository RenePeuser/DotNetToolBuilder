using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.Template
{
    internal interface ITemplateExtractor
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}
