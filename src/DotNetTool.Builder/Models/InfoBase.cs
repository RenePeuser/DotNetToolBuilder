using System.Diagnostics;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    internal class InfoBase
    {
        public InfoBase(string value, string name)
        {
            Value = value;
            Name = name;
        }

        public string Value { get; set; }

        public string Name { get; set; }

        public string NormalizedName => Name.FirstCharToUpper();
    }
}
