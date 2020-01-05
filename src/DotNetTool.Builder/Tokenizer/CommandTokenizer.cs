namespace DotNetTool.Builder.Tokenizer
{
    using Extensions;
    using Tokens;

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