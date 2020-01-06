using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Tokenizer.Tokens;

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
