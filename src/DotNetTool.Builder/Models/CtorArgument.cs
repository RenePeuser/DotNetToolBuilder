using System.Diagnostics;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    internal class CtorArgument
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
