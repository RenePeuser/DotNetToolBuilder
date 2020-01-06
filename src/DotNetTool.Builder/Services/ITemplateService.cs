using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, Models.DotNetTool dotNetTool);
    }
}
