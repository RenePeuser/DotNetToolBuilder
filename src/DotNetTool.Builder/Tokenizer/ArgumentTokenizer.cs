using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Tokenizer
{
    internal class ArgumentTokenizer : ITokenizer
    {
        public Token GetToken(string value)
        {
            return new ArgumentToken(value);
        }

        public bool IsThisTokenizerFor(string value)
        {
            return value.ContainsAny('<', '>', '[', ']');
        }
    }
}
