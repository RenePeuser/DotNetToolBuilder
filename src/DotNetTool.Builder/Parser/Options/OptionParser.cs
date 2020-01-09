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
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly ICollectOptionAlias _collectOptionAlias;
        private readonly ICollectDescription _collectDescription;

        public OptionParser(ICollectTillInputCorrect collectTillInputCorrect, ICollectOptionAlias collectOptionAlias, ICollectDescription collectDescription)
        {
            _collectTillInputCorrect = collectTillInputCorrect;
            _collectOptionAlias = collectOptionAlias;
            _collectDescription = collectDescription;
        }

        public OptionInfo Parse(OptionToken optionToken, ArgumentInfo argumentInfo)
        {
            var alias = _collectOptionAlias.Invoke(optionToken);

            var value = optionToken.Value;
            var required = _collectTillInputCorrect.CollectTillInputIsValid($"Is your option required (r) or optional (o): '{value}'", input => input.ContainsAnyOf("o", "r"), input => $"Input: '{input}' is not valid. Only '(r)' or '(o)' is a valid input");
            var boolRequired = required.ToLower().Equals("r");

            var description = _collectDescription.Collect($"Please enter a description for your option: '{value}'");
            var optionName = value.TrimStart('-');
            var normalizedOptiontName = optionName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();
            var optionArgumentName = normalizedOptiontName.FirstCharToLower();

            var option = new OptionInfo(value, optionName, alias, description, boolRequired, argumentInfo, normalizedOptiontName, optionArgumentName);
            return option;
        }
    }
}
