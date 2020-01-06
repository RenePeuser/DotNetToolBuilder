using DotNetTool.Builder.Services.IO;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.Template
{
    internal class TemplateService : ITemplateService
    {
        private readonly IRenameFilesAndFolders _renameFilesAndFolders;

        public TemplateService(IRenameFilesAndFolders renameFilesAndFolders)
        {
            _renameFilesAndFolders = renameFilesAndFolders;
        }

        public void RenameAllIn(IDirectoryInfo targetDirectory, Models.DotNetTool dotNetTool)
        {
            // Solution and projects
            _renameFilesAndFolders.Rename(targetDirectory, "rps.template", dotNetTool.ProjectName);

            // DotNetTool name
            _renameFilesAndFolders.Rename(targetDirectory, "Rps", dotNetTool.NormalizedToolName);
            _renameFilesAndFolders.Rename(targetDirectory, "rps", dotNetTool.ToolName.ToLower());
        }
    }
}
