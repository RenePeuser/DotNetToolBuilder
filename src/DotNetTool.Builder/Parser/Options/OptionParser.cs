using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Parser.Options
{
    using System;

    public class OptionParser : IOptionParser
    {
        private readonly IConsoleService _consoleService;

        public OptionParser(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public bool IsThisParserFor(string value)
        {
            return value.StartsWith("-");
        }

        public OptionInfo Parse(string value, ArgumentInfo argument)
        {
            var splitted = value.TrimStart('-').Split('-');
            var suggestion = new string(splitted.Select(s => s.First()).ToArray());

            _consoleService.WriteInput($"Please enter an alias for your option: '{value}' suggestion: '-{suggestion}'");
            var alias = _consoleService.ReadLine();

            _consoleService.WriteInput($"Is your option required (r) or optional (o): '{value}'");
            var required = _consoleService.ReadLine();
            var boolRequired = required.ToLower().Equals("r");

            _consoleService.WriteInput($"Please enter a description for your option: '{value}'");
            var description = _consoleService.ReadLine();

            var optioName = value.TrimStart('-');
            var normalizedOptiontName = optioName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            var optionArgumentName = normalizedOptiontName.FirstCharToLower();

            var option = new OptionInfo(value, optioName, alias, description, boolRequired, argument,
                normalizedOptiontName, optionArgumentName);
            return option;
        }
    }
}
