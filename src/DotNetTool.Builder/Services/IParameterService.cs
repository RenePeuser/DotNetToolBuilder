using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IParameterService
    {
        ParameterInfo FindAlreadyExistingCommand(string command,
            ParameterInfo current);

        ArgumentInfo FindAlreadyExistingArgument(string argument,
            ParameterInfo current);

        OptionInfo FindAlreadyExistingOption(string option,
            ParameterInfo current);
    }
}