namespace DotNetTool.Builder.Parser.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Services;
    using Tokenizer.Tokens;

    public class CommandParser : ICommandParser
    {
        private readonly IConsoleService _consoleService;

        public CommandParser(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public CommandInfo Parse(CommandToken commandToken, IEnumerable<OptionInfo> options)
        {
            var value = commandToken.Value;
            var parameter = new CommandInfo(value, value);
            _consoleService.WriteInput($"Please enter a description for your command: '{parameter.Name}'");
            var description = _consoleService.ReadLine();
            parameter.Description = description;
            parameter.Options = options.ToList();

            return parameter;
        }

        public CommandInfo Parse(CommandToken commandToken, IEnumerable<OptionInfo> options, CommandInfo parameterInfo)
        {
            var value = commandToken.Value;
            var parameter = new CommandInfo(value, value);
            parameter.Description = parameterInfo.Description;
            parameter.Options = parameterInfo.Options.ToList();
            return parameter;
        }
    }
}
