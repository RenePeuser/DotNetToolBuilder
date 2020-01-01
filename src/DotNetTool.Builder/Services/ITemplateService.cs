namespace DotNetTool.Builder.Services
{
    using FileSystemAbstraction;
    using Models;

    public interface ITemplateService
    {
        void RenameAllIn(IDirectoryInfo targetDirectory, DotNetTool dotNetTool);
    }
}