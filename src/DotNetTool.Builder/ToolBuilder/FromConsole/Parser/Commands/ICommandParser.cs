using System.Collections.Generic;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Commands
{
    internal interface ICommandParser
    {
        CommandInfo Parse(CommandToken commandToken, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, CommandInfo lastCommand, CommandInfo alreadyExistingCommand, CommandInfo previousExpressionCommand);
    }
}
