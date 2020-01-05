using System;
using System.IO;
using DotNetTool.Builder.ErrorHandling;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    internal class TargetFolderService : ITargetFolderService
    {
        private readonly IDirectoryService _directoryService;

        public TargetFolderService(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
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
            return targetDirectory;
        }
    }
}