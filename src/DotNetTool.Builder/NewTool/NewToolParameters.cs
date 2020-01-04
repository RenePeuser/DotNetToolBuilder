using System.IO;

namespace DotNetTool.Builder.NewTool
{
    internal class NewToolParameters
    {
        public FileInfo File { get; }

        public bool NoVisualStudio { get; }

        public NewToolParameters(FileInfo file, bool noVisualStudio)
        {
            File = file;
            NoVisualStudio = noVisualStudio;
        }
    }
}