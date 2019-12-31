namespace DotNetTool.Builder.Tokenizer
{
    using Extensions;
    using Tokens;

    public class ArgumentTokenizer : ITokenizer
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