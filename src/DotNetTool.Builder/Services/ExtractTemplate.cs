using System.IO;
using System.IO.Compression;
using DotNetTool.Builder.FileSystemAbstraction;
using DotNetTool.Builder.FileSystemAbstraction.Services;

namespace DotNetTool.Builder.Services
{
    public class ExtractTemplate : IExtractTemplate
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;

        public ExtractTemplate(IDirectoryService directoryService, IFileService fileService)
        {
            _directoryService = directoryService;
            _fileService = fileService;
        }

        public void ExtractTo(IDirectoryInfo directoryInfo)
        {
            var currentAssemblyLocation = _fileService.GetFileInfo(this.GetType().Assembly.Location);
            var templateAsZip = _fileService.GetFileInfo(Path.Combine(currentAssemblyLocation.Directory.FullName, "template.zip"));
            using var zipFile = ZipFile.OpenRead(templateAsZip.FullName);
            zipFile.ExtractToDirectory(directoryInfo.FullName, true);
        }
    }
}