using System.Diagnostics;

namespace DotNetTool.Builder.Tokenizer.Tokens
{
    [DebuggerDisplay("{" + nameof(Value) + "}")]
    public abstract class Token
    {
        public Token(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
