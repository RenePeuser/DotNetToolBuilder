namespace trumpf.hmi.dotnettool.builder
{
    internal class CtorArgument
    {
        public CtorArgument(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public string Type { get; }
        public string Name { get; }
    }
}