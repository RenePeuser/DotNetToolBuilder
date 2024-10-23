using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(MethodName) + "}")]
    internal sealed class MethodInfo(string methodName,
                                     string methodSyntax)
    {
        public string MethodName { get; } = methodName;

        public string MethodSyntax { get; } = methodSyntax;
    }
}
