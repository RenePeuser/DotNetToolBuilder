using System.Linq;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Services
{
    public class ParameterService : IParameterService
    {
        public ParameterInfo FindAlreadyExistingCommand(string command,
            ParameterInfo current)
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

        public ArgumentInfo FindAlreadyExistingArgument(string argument,
            ParameterInfo current)
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

        public OptionInfo FindAlreadyExistingOption(string option,
            ParameterInfo current)
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