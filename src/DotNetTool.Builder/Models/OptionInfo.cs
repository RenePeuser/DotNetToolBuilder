namespace DotNetTool.Builder.Models
{
    public class OptionInfo
    {
        public OptionInfo(string value, string name, string alias, string description, bool required,
            ArgumentInfo argument, string normalizedValue, string argumentName)
        {
            Value = value;
            Name = name;
            Alias = alias;
            Description = description;
            Required = required;
            Argument = argument;
            NormalizedValue = normalizedValue;
            ArgumentName = argumentName;
        }

        public string Value { get; }

        public string Name { get; }

        public string Alias { get; }

        public string Description { get; }

        public bool Required { get; }

        public ArgumentInfo Argument { get; }

        public string NormalizedValue { get; }

        public string ArgumentName { get; }
    }
}