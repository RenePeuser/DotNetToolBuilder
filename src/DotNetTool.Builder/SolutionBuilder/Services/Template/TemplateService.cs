using DotNetTool.Builder.SolutionBuilder.Services.IO;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.Services.Template
{
    internal sealed class TemplateService(IRenameFilesAndFolders renameFilesAndFolders) : ITemplateService
    {
        public void RenameAllIn(IDirectoryInfo targetDirectory, Models.DotNetTool dotNetTool)
        {
            // Solution and projects
            renameFilesAndFolders.Rename(targetDirectory, "rps.template", dotNetTool.ProjectName);

            // DotNetTool name
            renameFilesAndFolders.Rename(targetDirectory, "rps-command-name", dotNetTool.DotNetToolName.Name.ToLower());


            var dotNetToolName = dotNetTool.DotNetToolName.NormalizedName.ToLower();
            var useToolCommandHelp = dotNetTool.DotNetToolName.Name.ToLower().StartsWith("dotnet-") ? $"dotnet {dotNetToolName}" : dotNetToolName;
            renameFilesAndFolders.Rename(targetDirectory, "rps-command", useToolCommandHelp);

            // class etc. and rest
            renameFilesAndFolders.Rename(targetDirectory, "Rps", dotNetTool.DotNetToolName.NormalizedName);
            renameFilesAndFolders.Rename(targetDirectory, "rps", dotNetToolName);
        }
    }
}
