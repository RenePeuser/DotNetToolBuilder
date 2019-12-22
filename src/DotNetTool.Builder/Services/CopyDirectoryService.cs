using System.IO;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.FileSystemAbstraction.Services;

namespace DotNetTool.Builder.Services
{
    public class CopyDirectoryService : ICopyDirectoryService
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;

        public CopyDirectoryService(IDirectoryService directoryService, IFileService fileService)
        {
            _directoryService = directoryService;
            _fileService = fileService;
        }

        public void CopyDirectory(IDirectoryInfo sourceDirectory, IDirectoryInfo targetDirectory)
        {
            CopyDirectory(sourceDirectory, targetDirectory, true);
        }

        public void CopyDirectory(IDirectoryInfo sourceDirectory, IDirectoryInfo targetDirectory, bool copySubDirs)
        {
            if (sourceDirectory.NotExists())
            {
                throw new DirectoryNotFoundException(
                    $"Source directory does not exist or could not be found: '{sourceDirectory.FullName}'");
            }

            targetDirectory.NotExists().IfTrueThen(targetDirectory.Create);

            var files = sourceDirectory.EnumerateFiles();
            foreach (var file in files)
            {
                var targetPath = Path.Combine(targetDirectory.FullName, file.Name);
                file.CopyTo(targetPath, true);
            }

            if (copySubDirs)
            {
                var sourceDirectories = sourceDirectory.EnumerateDirectories();
                foreach (var subDirectory in sourceDirectories)
                {
                    var targetDirectoryPath =
                        _directoryService.GetDirectoryInfo(Path.Combine(targetDirectory.FullName, subDirectory.Name));
                    CopyDirectory(subDirectory, targetDirectoryPath, copySubDirs);
                }
            }
        }
    }
}
