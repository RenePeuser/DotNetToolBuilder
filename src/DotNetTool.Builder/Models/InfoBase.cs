using System.Diagnostics;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    internal class InfoBase
    {
        internal InfoBase(string value, string name)
        {
            Value = value;
            Name = name;
            NormalizedName = Name.FirstCharToUpper();
        }

        internal string Value { get; }

        internal string Name { get; }

        internal string NormalizedName { get; }
    }
}
