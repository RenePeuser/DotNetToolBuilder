using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    using Tokenizer.Tokens;

    public class ParameterService : IParameterService
    {
        public CommandInfo FindAlreadyExistingCommand(CommandToken command,
            CommandInfo current)
        {
            if (command.IsNull())
            {
                return null;
            }

            if (current.IsNull())
            {
                return null;
            }

            if (current.Name == command.Value)
            {
                return current;
            }

            if (current.SubCommands.IsNotNull())
            {
                foreach (var cliParameterInfo in current.SubCommands)
                {
                    if (cliParameterInfo.Name == command.Value)
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

            if (current.Argument.IsNotNull())
            {
                if (current.Argument.Value == argument.Name)
                {
                    return current.Argument;
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
