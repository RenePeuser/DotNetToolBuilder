
using DotNetTool.Builder.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.Tokenizer
{
    internal class CommandTokenizer : ITokenizer
    {
        public Token GetToken(string value)
        {
            return new CommandToken(value);
        }

        public bool IsThisTokenizerFor(string value)
        {
            return value.ContainsNotAnyOf("<", ">", "[", "]", "--");
        }
    }
}
