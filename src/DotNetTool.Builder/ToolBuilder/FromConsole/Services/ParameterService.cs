using System;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Services
{
    internal class ParameterService : IParameterService
    {
        public CommandInfo FindAlreadyExistingCommand(CommandToken commandToken,
            CommandInfo commandInfo)
        {
            Throw.IfNull(() => commandToken);

            if (commandInfo.IsNull())
            {
                return null;
            }

            return Find(commandInfo, cmdInfo => cmdInfo.Name == commandToken.Value ? cmdInfo : default);
        }

        public ArgumentInfo FindAlreadyExistingArgument(ArgumentInfo argumentInfo,
            CommandInfo commandInfo)
        {
            Throw.IfNull(() => argumentInfo);

            if (commandInfo.IsNull())
            {
                return null;
            }

            return Find(commandInfo, cmdInfo => cmdInfo?.Argument?.Value == argumentInfo.Name ? cmdInfo?.Argument : default);
        }

        public OptionInfo FindAlreadyExistingOption(OptionInfo option,
            CommandInfo commandInfo)
        {
            Throw.IfNull(() => option);

            if (commandInfo.IsNull())
            {
                return null;
            }

            return Find(commandInfo, cmdInfo =>
            {
                var existingOption = cmdInfo.Options.FirstOrDefault(o => o.Value == option.Value);
                if (existingOption.IsNotNull())
                {
                    return existingOption;
                }

                return null;
            });
        }

        public T Find<T>(CommandInfo commandInfo, Func<CommandInfo, T> findPredicate)
        {
            if (findPredicate.IsNull())
            {
                return default;
            }

            if (commandInfo.IsNull())
            {
                return default;
            }

            var result = findPredicate(commandInfo);
            if (result.IsNotNull())
            {
                return result;
            }

            if (commandInfo.SubCommands.IsNotNull())
            {
                foreach (var subcCommand in commandInfo.SubCommands)
                {
                    var recursiveResult = Find(subcCommand, findPredicate);
                    if (recursiveResult.IsNotNull())
                    {
                        return recursiveResult;
                    }
                }
            }

            return default;
        }
    }
}
