using System.IO.Compression;
using System.Linq;
using DotNetTool.Builder.FileSystemAbstraction;

namespace DotNetTool.Builder.Services
{
    public class ExtractTemplate : IExtractTemplate
    {
        public void ExtractTo(IDirectoryInfo directoryInfo)
        {
            var names = this.GetType().Assembly.GetManifestResourceNames();
            var template = names.First(n => n.Contains("template.zip"));
            using var stream = this.GetType().Assembly.GetManifestResourceStream(template);
            using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
            archive.ExtractToDirectory(directoryInfo.FullName, true);
        }
    }
}