using System.IO.Compression;
using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public class ExtractTemplate : IExtractTemplate
    {
        public void ExtractTo(IDirectoryInfo directoryInfo)
        {
            using var stream = this.GetType().Assembly.GetManifestResourceStream("DotNetTool.Builder.template.zip");
            using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
            archive.ExtractToDirectory(directoryInfo.FullName, true);
        }
    }
}