using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Argument
{
    using Tokenizer.Tokens;

    internal interface IArgumentParser
    {
        ArgumentInfo Parse(ArgumentToken value);
    }
}
