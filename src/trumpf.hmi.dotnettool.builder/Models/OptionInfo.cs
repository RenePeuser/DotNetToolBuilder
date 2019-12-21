namespace trumpf.hmi.dotnettool.builder.Models
{
    public class OptionInfo
    {
        public OptionInfo(string value, string name, string alias, string description, bool required, Argument argument, string normalizedValue, string argumentName)
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

        public Argument Argument { get; }

        public string NormalizedValue { get; }

        public string ArgumentName { get; }
    }
}