using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer
{
    internal sealed class CommandTokenizer : ITokenizer
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
