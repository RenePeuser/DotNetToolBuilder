using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using Models;

    internal interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, DotNetTool dotNetTool);
    }
}