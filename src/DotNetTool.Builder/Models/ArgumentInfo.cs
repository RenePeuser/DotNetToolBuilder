namespace DotNetTool.Builder.Models
{
    public class ArgumentInfo
    {
        public ArgumentInfo(string name, string description, string value, string normalizedName, string type)
        {
            Name = name;
            Description = description;
            Value = value;
            NormalizedName = normalizedName;
            Type = type;
        }

        public string Name { get; }

        public string Description { get; }

        public string Value { get; }

        public string NormalizedName { get; }

        public string Type { get; }
    }
}