using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.Services.Template
{
    internal interface ITemplateExtractor
    {
        void ExtractTo(IDirectoryInfo directoryInfo);
    }
}
