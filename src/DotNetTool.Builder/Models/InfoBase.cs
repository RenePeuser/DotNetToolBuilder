using System.Diagnostics;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public abstract class InfoBase
    {
        protected InfoBase(string value, string name)
        {
            Value = value;
            Name = name;
            NormalizedName = Name.FirstCharToUpper();
        }

        public string Value { get; }

        public string Name { get; }

        public string NormalizedName { get; }
    }
}
