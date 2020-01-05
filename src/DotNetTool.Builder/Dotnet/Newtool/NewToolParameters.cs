namespace DotNetTool.Builder.Dotnet.Newtool
{
    internal class NewToolParameters
    {
        internal NewToolParameters(System.IO.FileInfo fromFile, System.IO.DirectoryInfo saveToolToTo, bool useCode, bool useVisualStudio, bool useRider)
        {
            FromFile = fromFile;
            UseVsCode = useCode;
            UseVisualStudio = useVisualStudio;
            UseRider = useRider;
            SaveToolTo = saveToolToTo;
        }

        internal System.IO.FileInfo FromFile { get; }

        internal System.IO.DirectoryInfo SaveToolTo { get; }

        internal bool UseVsCode { get; }

        internal bool UseVisualStudio { get; }

        public bool UseRider { get; set; }
    }
}