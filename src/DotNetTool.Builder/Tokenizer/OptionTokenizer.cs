namespace DotNetTool.Builder.Tokenizer
{
    using Tokens;

    internal class OptionTokenizer : ITokenizer
    {
        public Token GetToken(string value)
        {
            return new OptionToken(value);
        }

        public bool IsThisTokenizerFor(string value)
        {
            return value.StartsWith("--");
        }
    }
}