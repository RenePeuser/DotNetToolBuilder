using System.IO;

namespace DotNetTool.Builder.DotNet.Newtool
{
    internal sealed class NewToolParameters
    {
        internal NewToolParameters(FileInfo fromFile, DirectoryInfo saveToolToTo, bool useCode, bool useVisualStudio, bool useRider, FileInfo targetZipFileInfo, bool useFastMode)
        {
            FromFile = fromFile;
            UseVsCode = useCode;
            UseVisualStudio = useVisualStudio;
            UseRider = useRider;
            SaveToolTo = saveToolToTo;
            TargetZipFileInfo = targetZipFileInfo;
            UseFastMode = useFastMode;
        }

        internal FileInfo FromFile { get; }

        internal DirectoryInfo SaveToolTo { get; }

        internal bool UseVsCode { get; }

        internal bool UseVisualStudio { get; }

        internal bool UseRider { get; }

        internal bool UseFastMode { get; }

        internal FileInfo TargetZipFileInfo { get; }
    }
}
