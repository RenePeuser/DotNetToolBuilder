namespace DotNetTool.Builder.Models
{
    public class DotNetToolInfo
    {
        public DotNetToolInfo(string name, string version, string command)
        {
            Name = name;
            Version = version;
            Command = command;
        }

        public string Name { get; }

        public string Version { get; }

        public string Command { get; }
    }
}
