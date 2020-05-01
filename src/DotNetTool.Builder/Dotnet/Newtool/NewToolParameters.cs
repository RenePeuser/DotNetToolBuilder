using System.IO;

namespace DotNetTool.Builder.DotNet.Newtool
{
    internal class NewToolParameters
    {
        internal NewToolParameters(FileInfo fromFile, DirectoryInfo saveToolToTo, bool useCode, bool useVisualStudio, bool useRider, bool packAsZip)
        {
            FromFile = fromFile;
            UseVsCode = useCode;
            UseVisualStudio = useVisualStudio;
            UseRider = useRider;
            SaveToolTo = saveToolToTo;
            PackAsZip = packAsZip;
        }

        internal FileInfo FromFile { get; }

        internal DirectoryInfo SaveToolTo { get; }

        internal bool UseVsCode { get; }

        internal bool UseVisualStudio { get; }

        internal bool UseRider { get; }

        internal bool PackAsZip { get; }
    }
}
