using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(InterfaceType) + "} - " + "{" + nameof(ImplementationType) + "}")]
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
