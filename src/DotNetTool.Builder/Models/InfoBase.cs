namespace DotNetTool.Builder.Models
{
    using System.Diagnostics;
    using Extensions;

    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public class InfoBase
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