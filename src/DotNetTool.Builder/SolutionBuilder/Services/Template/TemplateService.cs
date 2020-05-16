using DotNetTool.Builder.SolutionBuilder.Services.IO;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.Services.Template
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
            _renameFilesAndFolders.Rename(targetDirectory, "rps-command-name", dotNetTool.DotNetToolName.Name.ToLower());


            var dotNetToolName = dotNetTool.DotNetToolName.NormalizedName.ToLower();
            var useToolCommandHelp = dotNetTool.DotNetToolName.Name.ToLower().StartsWith("dotnet-") ? $"dotnet {dotNetToolName}" : dotNetToolName;
            _renameFilesAndFolders.Rename(targetDirectory, "rps-command", useToolCommandHelp);

            // class etc. and rest
            _renameFilesAndFolders.Rename(targetDirectory, "Rps", dotNetTool.DotNetToolName.NormalizedName);
            _renameFilesAndFolders.Rename(targetDirectory, "rps", dotNetToolName);
        }
    }
}
