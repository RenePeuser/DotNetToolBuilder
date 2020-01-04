namespace DotNetTool.Builder.Dotnet.Newtool
{
    internal class NewToolParameters
    {
        public NewToolParameters(System.IO.FileInfo fromFile, bool openVisualstudio)
        {
            FromFile = fromFile;
            OpenVisualstudio = openVisualstudio;
        }

        public System.IO.FileInfo FromFile { get; }
        public bool OpenVisualstudio { get; }
    }
}