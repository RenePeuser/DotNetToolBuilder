using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser.Options
{
    internal sealed class OptionParser : IOptionParser
    {
        private readonly ICollectDescription _collectDescription;
        private readonly ICollectOptionAlias _collectOptionAlias;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

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
            var required = _collectTillInputCorrect.CollectTillInputIsValid($"Is your option required (r) or optional (o): '{value}'", input => input.EqualsAnyOf("o", "r"), input => $"Input: '{input}' is not valid. Only '(r)' or '(o)' is a valid input");
            var boolRequired = required.ToLower().Equals("r");

            var description = _collectDescription.Collect($"Please enter a description for your option: '{value}'");
            var optionName = value.TrimStart('-');
            var normalizedOptiontName = optionName.Split('-').Select(s => s.FirstCharToUpper()).Flatten();

            var option = new OptionInfo(value, optionName, alias, description, boolRequired, argumentInfo, normalizedOptiontName);
            return option;
        }
    }
}
