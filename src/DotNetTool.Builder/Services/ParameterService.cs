using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public class ParameterService : IParameterService
    {
        public CommandInfo FindAlreadyExistingCommand(CommandInfo command,
            CommandInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }

            if (current.Name == command.Name)
            {
                return current;
            }

            if (current.SubCommands.IsNotNull())
            {
                foreach (var cliParameterInfo in current.SubCommands)
                {
                    if (cliParameterInfo.Name == command.Name)
                    {
                        return cliParameterInfo;
                    }

                    var match = FindAlreadyExistingCommand(command, cliParameterInfo);
                    if (match.IsNotNull())
                    {
                        return match;
                    }
                }
            }

            return null;
        }

        public ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argument,
            CommandInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }

            if (current.ArgumentInfo.IsNotNull())
            {
                if (current.ArgumentInfo.Value == argument.Name)
                {
                    return current.ArgumentInfo;
                }
            }

            if (current.SubCommands.IsNotNull())
            {
                foreach (var subCommand in current.SubCommands)
                {
                    var match = FindAlreadyExistingArgument(argument, subCommand);
                    if (match.IsNotNull())
                    {
                        return match;
                    }
                }
            }


            return null;
        }

        public OptionInfo FindAlreadyExistingOption(OptionInfo option,
            CommandInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }


            var existingOption = current.Options.FirstOrDefault(o => o.Value == option.Value);
            if (existingOption.IsNotNull())
            {
                return existingOption;
            }

            if (current.SubCommands.IsNotNull())
            {
                foreach (var subCommand in current.SubCommands)
                {
                    var match = FindAlreadyExistingOption(option, subCommand);
                    if (match.IsNotNull())
                    {
                        return match;
                    }
                }
            }

            return null;
        }
    }
}
