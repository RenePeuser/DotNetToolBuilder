using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer
{
    internal sealed class ArgumentTokenizer : ITokenizer
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
