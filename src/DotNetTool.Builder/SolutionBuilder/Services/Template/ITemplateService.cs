using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.Services.Template
{
    internal interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, Models.DotNetTool dotNetTool);
    }
}
