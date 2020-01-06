using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.Template
{
    internal interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, Models.DotNetTool dotNetTool);
    }
}
