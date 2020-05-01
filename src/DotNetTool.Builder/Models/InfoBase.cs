using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public abstract class InfoBase
    {
        protected InfoBase(string value, string name, string normalizedName)
        {
            Value = value;
            Name = name;
            NormalizedName = normalizedName;
        }

        public string Value { get; }

        public string Name { get; }

        public string NormalizedName { get; set; }
    }
}
