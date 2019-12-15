using System.Collections.Generic;
using System.Linq;
using trumpf.hmi.dotnettool.builder.Builder.Commands;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class CliParameterService
    {
        public static CliParameterInfo FindAlreadyExistingCommand(IEnumerable<CliParameterInfo> others,
            CliParameterInfo current)
        {
            if (others.IsNull())
            {
                return null;
            }

            foreach (var cliParameterInfo in others)
            {
                if (cliParameterInfo.Name == current.Name)
                {
                    return cliParameterInfo;
                }

                var match = FindAlreadyExistingCommand(cliParameterInfo.SubCommands, current);
                if (match.IsNotNull())
                {
                    return match;
                }
            }

            return null;
        }

        public static CliParameterInfo FindAlreadyExistingCommand(string command,
            CliParameterInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }

            if (current.Name == command)
            {
                return current;
            }

            if (current.SubCommands.IsNotNull())
            {
                foreach (var cliParameterInfo in current.SubCommands)
                {
                    if (cliParameterInfo.Name == command)
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

        public static Argument FindAlreadyExistingArgument(string argument,
            CliParameterInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }

            if (current.ArgumentInfo.IsNotNull())
            {
                if (current.ArgumentInfo.Value == argument)
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

        public static OptionInfo FindAlreadyExistingOption(string option,
            CliParameterInfo current)
        {
            if (current.IsNull())
            {
                return null;
            }


            var existingOption = current.Options.FirstOrDefault(o => o.Value == option);
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