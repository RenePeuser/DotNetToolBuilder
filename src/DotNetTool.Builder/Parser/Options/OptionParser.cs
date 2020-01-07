using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Options
{
    internal class OptionParser : IOptionParser
    {
        private readonly IConsoleService _consoleService;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

        public OptionParser(IConsoleService consoleService, ICollectTillInputCorrect collectTillInputCorrect)
        {
            _consoleService = consoleService;
            _collectTillInputCorrect = collectTillInputCorrect;
        }

        public OptionInfo Parse(OptionToken token, ArgumentInfo argument)
        {
            var value = token.Value;
            var splitted = value.TrimStart('-').Split('-');
            var suggestion = new string(splitted.Select(s => s.First()).ToArray());

            _consoleService.WriteInput($"Please enter an alias for your option: '{value}' suggestion: '-{suggestion}'");
            var alias = _consoleService.ReadLine();

            string required = _collectTillInputCorrect.CollectTillUserInputOk($"Is your option required (r) or optional (o): '{value}'", "o", "r");
            var boolRequired = required.ToLower().Equals("r");

            _consoleService.WriteInput($"Please enter a description for your option: '{value}'");
            var description = _consoleService.ReadLine();

            var optioName = value.TrimStart('-');
            var normalizedOptiontName = optioName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            var optionArgumentName = normalizedOptiontName.FirstCharToLower();

            var option = new OptionInfo(value, optioName, alias, description, boolRequired, argument, normalizedOptiontName, optionArgumentName);
            return option;
        }
    }
}
