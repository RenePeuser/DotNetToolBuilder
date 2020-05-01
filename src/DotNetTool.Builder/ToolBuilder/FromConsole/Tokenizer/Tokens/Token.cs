using System.Diagnostics;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    internal abstract class Token
    {
        internal Token(string value)
        {
            Value = value;
        }

        internal string Value { get; }
    }
}
