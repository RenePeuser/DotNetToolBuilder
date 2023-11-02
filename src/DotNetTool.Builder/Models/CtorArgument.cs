using System.Diagnostics;

using Extensions.Pack;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    internal sealed class CtorArgument
    {
        public CtorArgument(string type, string name)
        {
            Type = type;
            Name = name;
            NormalizedName = name.FirstCharToUpper();
        }

        public string Type { get; }

        public string Name { get; }

        public string NormalizedName { get; }
    }
}
