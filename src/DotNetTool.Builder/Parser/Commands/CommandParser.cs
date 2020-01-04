using Argument.Check;

namespace DotNetTool.Builder.Parser.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    
    using Models;
    using Services;
    using Tokenizer.Tokens;

    public class CommandParser : ICommandParser
    {
        private readonly IConsoleService _consoleService;

        public CommandParser(IConsoleService consoleService)
        {
            Throw.IfNull(() => consoleService);

            _consoleService = consoleService;
        }


        public CommandInfo Parse(CommandToken commandToken, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, CommandInfo lastCommand, CommandInfo alreadyExistingCommand, CommandInfo previousExpressionCommand)
        {
            // Null allowed
            // Throw.IfNull(() => argumentInfo);
            // Throw.IfNull(() => previousCommand);
            Throw.IfNull(() => commandToken);
            Throw.IfNull(() => options);

            var value = commandToken.Value;

            if (alreadyExistingCommand.IsNull())
            {
                _consoleService.WriteInput($"Please enter a description for your command: '{value}'");
                var description = _consoleService.ReadLine();
                var commands = lastCommand.IsNotNull() ? lastCommand.ToIList() : Enumerable.Empty<CommandInfo>();
                var command = new CommandInfo(value, value, description, argumentInfo, options, commands);
                return command;
            }

            var subCommands = Enumerable.Empty<CommandInfo>();
            var currentArgument = argumentInfo;
            var currentOptions = options;
            if (alreadyExistingCommand.Name.EqualsTo(value))
            {
                if (lastCommand.IsNull())
                {
                    subCommands = alreadyExistingCommand.SubCommands.ToList();
                    currentArgument = alreadyExistingCommand.Argument.IsNull() ? currentArgument : alreadyExistingCommand.Argument;
                }
                else
                {
                    if (alreadyExistingCommand.SubCommands.All(s => s.Name.Equals(lastCommand.Name)))
                    {
                        subCommands = lastCommand.ToIList();
                    }
                    else
                    {
                        subCommands = alreadyExistingCommand.SubCommands.Concat(lastCommand).ToList();
                    }

                    currentArgument = alreadyExistingCommand.Argument;
                    currentOptions = alreadyExistingCommand.Options;
                }
            }

            var result = new CommandInfo(alreadyExistingCommand.Value, alreadyExistingCommand.Name, alreadyExistingCommand.Description, currentArgument, currentOptions, subCommands);
            return result;

        }
    }
}
