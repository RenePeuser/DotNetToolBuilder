using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    using Tokenizer.Tokens;

    public interface IParameterService
    {
        CommandInfo FindAlreadyExistingCommand(CommandToken command, CommandInfo current);

        ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argument, CommandInfo current);

        OptionInfo FindAlreadyExistingOption(OptionInfo option, CommandInfo current);
    }
}
