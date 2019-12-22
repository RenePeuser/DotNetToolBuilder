namespace DotNetTool.Builder.Models
{
    public class TypeToRegister
    {
        public TypeToRegister(string interfaceType, string implementationType)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
        }

        public string InterfaceType { get; }
        public string ImplementationType { get; }
    }
}
