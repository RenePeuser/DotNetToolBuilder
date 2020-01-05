namespace DotNetTool.Builder.Tokenizer
{
    using Tokens;

    internal interface ITokenizer
    {
        Token GetToken(string value);
        bool IsThisTokenizerFor(string value);
    }
}