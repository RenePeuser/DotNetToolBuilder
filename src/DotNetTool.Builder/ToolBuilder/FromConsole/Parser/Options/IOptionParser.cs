using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Options
{
    internal interface IOptionParser
    {
        OptionInfo Parse(OptionToken optionToken, ArgumentInfo argumentInfo);
    }
}
