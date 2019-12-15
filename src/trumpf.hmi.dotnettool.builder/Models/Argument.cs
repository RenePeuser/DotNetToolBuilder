namespace trumpf.hmi.dotnettool.builder.Models
{
    public class Argument
    {
        public Argument(string name, string description, string value, string normalizedName)
        {
            Name = name;
            Description = description;
            Value = value;
            NormalizedName = normalizedName;
        }

        public string Name { get; }

        public string Description { get; }

        public string Value { get; }

        public string NormalizedName { get; }
    }
}