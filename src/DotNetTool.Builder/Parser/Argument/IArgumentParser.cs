using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Argument
{
    using Tokenizer.Tokens;

    public interface IArgumentParser
    {
        ArgumentInfo Parse(ArgumentToken value);
    }
}
