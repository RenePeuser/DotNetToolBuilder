using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    internal sealed class Property(string type,
                                   string name)
    {
        public string Type { get; } = type;

        public string Name { get; } = name;
    }
}
