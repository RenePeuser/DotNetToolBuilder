namespace DotNetTool.Builder.Parser.Commands
{
    using System.Collections.Generic;
    using Models;
    using Tokenizer.Tokens;

    public interface ICommandParser
    {
        CommandInfo Parse(CommandToken commandToken, IEnumerable<OptionInfo> options);
        CommandInfo Parse(CommandToken commandToken, IEnumerable<OptionInfo> options, CommandInfo parameterInfo);
    }
}
