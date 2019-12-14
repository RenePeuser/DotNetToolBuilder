namespace trumpf.hmi.dotnettool.builder
{
    public class Argument
    {
        public Argument(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}