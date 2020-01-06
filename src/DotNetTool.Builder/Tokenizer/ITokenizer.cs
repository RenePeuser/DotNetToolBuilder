using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Tokenizer
{
    internal interface ITokenizer
    {
        Token GetToken(string value);
        bool IsThisTokenizerFor(string value);
    }
}
