using System.IO.Compression;
using System.Linq;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Services.Template
{
    internal class TemplateExtractor : ITemplateExtractor
    {
        public void ExtractTo(IDirectoryInfo directoryInfo)
        {
            var assembly = GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var templateResourceName = resourceNames.Single(resource => resource.Contains("dotnet.tool.builder.template"));

            using var templateStream = assembly.GetManifestResourceStream(templateResourceName);
            using var zipArchive = new ZipArchive(templateStream);
            zipArchive.ExtractToDirectory(directoryInfo.FullName, true);
        }
    }
}
