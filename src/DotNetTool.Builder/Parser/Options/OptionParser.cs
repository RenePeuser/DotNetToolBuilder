using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Parser.Options
{
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
            _consoleService.WriteLine();
            _consoleService.WriteLine($"Please enter an alias for your option: '{value}'".AsInput());
            var alias = _consoleService.ReadLine();
            _consoleService.WriteLine();

            _consoleService.WriteLine($"Is your option required (r) or optional (o): '{value}'".AsInput());
            var required = _consoleService.ReadLine();
            var boolRequired = required.ToLower().Equals("r");

            _consoleService.WriteLine();
            _consoleService.WriteLine($"Please enter a description for your option: '{value}'".AsInput());
            var description = _consoleService.ReadLine();

            var optioName = value.TrimStart('-');
            var normalizedOptiontName = optioName.Split('-').Select(s => StringExtensions.FirstCharToUpper(s)).Flatten();
            var optionArgumentName = normalizedOptiontName.FirstCharToLower();

            var option = new OptionInfo(value, optioName, alias, description, boolRequired, argument, normalizedOptiontName, optionArgumentName);
            return option;
        }
    }
}