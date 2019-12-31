namespace DotNetTool.Builder.Tokenizer
{
    using Tokens;

    public interface ITokenizer
    {
        Token GetToken(string value);
        bool IsThisTokenizerFor(string value);
    }
}