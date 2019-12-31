namespace DotNetTool.Builder.Parser.Commands
{
    using System.Collections.Generic;
    using Models;
    using Tokenizer.Tokens;

    public interface ICommandParser
    {
        CommandInfo Parse(CommandToken commandToken, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, CommandInfo lastCommand, CommandInfo alreadyExistingCommand, CommandInfo previousExpressionCommand);
    }
}
