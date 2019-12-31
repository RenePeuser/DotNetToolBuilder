namespace DotNetTool.Builder.Tokenizer.Tokens
{
    using System.Diagnostics;

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