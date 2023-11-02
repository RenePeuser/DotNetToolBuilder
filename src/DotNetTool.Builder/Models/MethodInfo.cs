using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(MethodName) + "}")]
    internal sealed class MethodInfo
    {
        public MethodInfo(string methodName, string methodSyntax)
        {
            MethodName = methodName;
            MethodSyntax = methodSyntax;
        }

        public string MethodName { get; }

        public string MethodSyntax { get; }
    }
}
