using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    internal sealed class Property
    {
        public Property(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}
