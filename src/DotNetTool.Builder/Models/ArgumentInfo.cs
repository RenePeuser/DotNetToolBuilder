using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    public class ArgumentInfo : InfoBase
    {
        public ArgumentInfo(string name, string description, string value, string normalizedName, string type) : base(value, name)
        {
            Description = description;
            NormalizedParameterName = normalizedName.FirstCharToLower();
            Type = type;
        }

        public string Description { get; }

        public string NormalizedParameterName { get; }

        public string Type { get; }
    }
}
