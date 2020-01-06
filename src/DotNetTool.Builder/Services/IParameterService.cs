using System;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Services
{
    internal interface IParameterService
    {
        CommandInfo FindAlreadyExistingCommand(CommandToken commandToken, CommandInfo commandInfo);

        ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argumentInfo, CommandInfo commandInfo);

        OptionInfo FindAlreadyExistingOption(OptionInfo option, CommandInfo current);
        T Find<T>(CommandInfo commandInfo, Func<CommandInfo, T> findPredicate);
    }
}
