using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Argument
{
    internal interface IArgumentParser
    {
        ArgumentInfo Parse(ArgumentToken value);
    }
}
