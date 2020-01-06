using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Tokenizer
{
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
