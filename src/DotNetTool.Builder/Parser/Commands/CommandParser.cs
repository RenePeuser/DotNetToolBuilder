using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Commands
{
    internal class CommandParser : ICommandParser
    {
        private readonly ICollectDescription _collectDescription;
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public CommandParser(ICollectDescription collectDescription, IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            Throw.IfNull(() => collectDescription);

            _collectDescription = collectDescription;
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public CommandInfo Parse(CommandToken commandToken, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, CommandInfo lastCommand, CommandInfo alreadyExistingCommand, CommandInfo previousExpressionCommand)
        {
            // Null allowed
            // Throw.IfNull(() => argumentInfo);
            // Throw.IfNull(() => previousCommand);
            // Throw.IfNull(() => lastCommand);
            // Throw.IfNull(() => alreadyExistingCommand);
            // Throw.IfNull(() => previousExpressionCommand);
            Throw.IfNull(() => options);
            Throw.IfNull(() => commandToken);

            var value = commandToken.Value;
            var normalizedValue = _dotNetToolNameNormalizer.Normalize(value);

            if (alreadyExistingCommand.IsNull())
            {
                var description = _collectDescription.Collect($"Please enter a description for your command: '{value}'");
                var commands = lastCommand.IsNotNull() ? lastCommand.ToIList() : Enumerable.Empty<CommandInfo>();
                var command = new CommandInfo(value, value, normalizedValue, description, argumentInfo, options, commands);
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
                    subCommands = alreadyExistingCommand.SubCommands.All(s => s.Name.Equals(lastCommand.Name)) ? lastCommand.ToIList() : alreadyExistingCommand.SubCommands.Concat(lastCommand).ToList();

                    currentArgument = alreadyExistingCommand.Argument;
                    currentOptions = alreadyExistingCommand.Options;
                }
            }

            var result = new CommandInfo(alreadyExistingCommand.Value, alreadyExistingCommand.Name, alreadyExistingCommand.NormalizedName, alreadyExistingCommand.Description, currentArgument, currentOptions, subCommands);
            return result;
        }
    }
}
