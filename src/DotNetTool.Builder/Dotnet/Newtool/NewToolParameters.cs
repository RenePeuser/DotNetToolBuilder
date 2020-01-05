namespace DotNetTool.Builder.Dotnet.Newtool
{
    internal class NewToolParameters
    {
        internal NewToolParameters(System.IO.FileInfo fromFile, bool usecode, bool usevisualstudio)
        {
            FromFile = fromFile;
            UseVsCode = usecode;
            UseVisualStudio = usevisualstudio;
        }

        internal System.IO.FileInfo FromFile { get; }

        internal bool UseVsCode { get; }

        internal bool UseVisualStudio { get; }
    }
}