namespace trumpf.hmi.dotnettool.builder
{
    public class OptionInfo
    {
        public OptionInfo(string value, string name, string alias, string description, bool required, Argument argument)
        {
            Value = value;
            Name = name;
            Alias = alias;
            Description = description;
            Required = required;
            Argument = argument;
        }

        public string Value { get; }
        public string Name { get; }
        public string Alias { get; }
        public string Description { get; }
        public bool Required { get; }
        public Argument Argument { get; }
    }
}