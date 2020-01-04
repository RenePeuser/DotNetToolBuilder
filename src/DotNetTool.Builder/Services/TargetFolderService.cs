using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services
{
    using System.IO;
    using Argument.Check;
    using Models;

    public class TargetFolderService : ITargetFolderService
    {
        private readonly IDirectoryService _directoryService;

        public TargetFolderService(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }

        public IDirectoryInfo CreateTargetDirectory(DotNetTool dotNetTool)
        {
            var currentDirectory = _directoryService.GetCurrentDirectory().FullName;
            var targetDirectory = _directoryService.GetDirectoryInfo(Path.Combine(currentDirectory, dotNetTool.ProjectName));

            Throw.If(() => targetDirectory, dir => dir.Exists, $"The directory: {targetDirectory.FullName} already exists.");

            targetDirectory.Create();
            return targetDirectory;
        }
    }
}