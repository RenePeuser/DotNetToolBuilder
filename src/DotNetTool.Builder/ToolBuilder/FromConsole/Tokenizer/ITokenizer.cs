using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer
{
    internal interface ITokenizer
    {
        Token GetToken(string value);
        bool IsThisTokenizerFor(string value);
    }
}
