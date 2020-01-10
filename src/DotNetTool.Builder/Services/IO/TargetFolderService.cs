using System;
using System.IO;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Extensions;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.IO
{
    internal class TargetFolderService : ITargetFolderService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;

        public TargetFolderService(IDirectoryService directoryService, IFileService fileService)
        {
            _directoryService = directoryService;
            _fileService = fileService;
        }

        public IDirectoryInfo CreateTargetDirectory(Models.DotNetTool dotNetTool)
        {
            var currentDirectory = _directoryService.GetCurrentDirectory().FullName;
            var targetDirectory = _directoryService.GetDirectoryInfo(Path.Combine(currentDirectory, dotNetTool.ProjectName));

            if (targetDirectory.Exists)
            {
                throw new DotNetToolBuilderException($"The directory: '{targetDirectory.FullName}' already exists.{Environment.NewLine}If you deserialize tool twice please rename the already existing folder or delete it");
            }

            targetDirectory.Create();
            var newTargetDirectory = _directoryService.GetDirectoryInfo(targetDirectory.FullName);
            return newTargetDirectory;
        }

        public IDirectoryInfo GetToolFolder(Models.DotNetTool dotNetTool, IDirectoryInfo targetDirectoryInfo)
        {
            if (targetDirectoryInfo.NotExists)
            {
                throw new DotNetToolBuilderException($"The directory: '{targetDirectoryInfo.FullName}' does not exists.");
            }

            var toolFolder = _directoryService.GetDirectoryInfo(Path.Combine(targetDirectoryInfo.FullName,"src", dotNetTool.ProjectName, dotNetTool.DotNetToolName.NormalizedName));
            if (toolFolder.NotExists)
            {
                throw new DotNetToolBuilderException($"The tool folder: '{toolFolder.FullName}' does not exists.");
            }

            return toolFolder;
        }

        public IFileInfo GetSolutionFile(Models.DotNetTool dotNetTool, IDirectoryInfo targetDirectoryInfo)
        {
            if (targetDirectoryInfo.NotExists)
            {
                throw new DotNetToolBuilderException($"The directory: '{targetDirectoryInfo.FullName}' does not exists.");
            }

            var solutionFile = _fileService.GetFileInfo(Path.Combine(targetDirectoryInfo.FullName, "src", $"{dotNetTool.ProjectName}.sln"));
            if (solutionFile.NotExists)
            {
                throw new DotNetToolBuilderException($"The solution file: '{solutionFile.FullName}' does not exists.");
            }

            return solutionFile;
        }
    }
}
