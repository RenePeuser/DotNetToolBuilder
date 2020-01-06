using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Services
{
    internal interface IParameterService
    {
        CommandInfo FindAlreadyExistingCommand(CommandToken command, CommandInfo current);

        ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argument, CommandInfo current);

        OptionInfo FindAlreadyExistingOption(OptionInfo option, CommandInfo current);
    }
}
