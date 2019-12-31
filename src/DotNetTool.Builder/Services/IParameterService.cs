using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IParameterService
    {
        CommandInfo FindAlreadyExistingCommand(CommandInfo command, CommandInfo current);

        ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argument, CommandInfo current);

        OptionInfo FindAlreadyExistingOption(OptionInfo option, CommandInfo current);
    }
}
