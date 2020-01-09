using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Options
{
    internal class OptionParser : IOptionParser
    {
        private readonly IConsoleService _consoleService;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly ICollectOptionAlias _collectOptionAlias;

        public OptionParser(IConsoleService consoleService, ICollectTillInputCorrect collectTillInputCorrect, ICollectOptionAlias collectOptionAlias)
        {
            _consoleService = consoleService;
            _collectTillInputCorrect = collectTillInputCorrect;
            _collectOptionAlias = collectOptionAlias;
        }

        public OptionInfo Parse(OptionToken optionToken, ArgumentInfo argumentInfo)
        {
            var alias = _collectOptionAlias.Invoke(optionToken);

            var value = optionToken.Value;
            string required = _collectTillInputCorrect.CollectTillInoutIsValid($"Is your option required (r) or optional (o): '{value}'", "o", "r");
            var boolRequired = required.ToLower().Equals("r");

            _consoleService.WriteInput($"Please enter a description for your option: '{value}'");
            var description = _consoleService.ReadLine();

            var optioName = value.TrimStart('-');
            var normalizedOptiontName = optioName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            var optionArgumentName = normalizedOptiontName.FirstCharToLower();

            var option = new OptionInfo(value, optioName, alias, description, boolRequired, argumentInfo, normalizedOptiontName, optionArgumentName);
            return option;
        }
    }
}
