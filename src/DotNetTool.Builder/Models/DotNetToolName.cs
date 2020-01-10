namespace DotNetTool.Builder.Models
{
    public class DotNetToolName
    {
        public DotNetToolName(string value) : this(value, value)
        {
        }

        public DotNetToolName(string value, string normalizedName)
        {
            Value = value;
            NormalizedName = normalizedName;
        }

        public string Value { get; }

        public string NormalizedName { get; }
    }
}