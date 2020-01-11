using Argument.Check;

namespace DotNetTool.Builder.Models
{
    public class DotNetToolName
    {
        public DotNetToolName(string value, string normalizedName)
        {
            Throw.IfNullOrWhiteSpace(() => value);
            Throw.IfNullOrWhiteSpace(() => normalizedName);

            Value = value;
            NormalizedName = normalizedName;
        }

        public string Value { get; }

        public string NormalizedName { get; }
    }
}
