using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Argument
{
    internal interface IArgumentParser
    {
        ArgumentInfo Parse(ArgumentToken value);
    }
}
