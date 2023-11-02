using System.Linq;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class CollectOptionAlias : ICollectOptionAlias
    {
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IOptionAliasValidator _inputValidator;

        public CollectOptionAlias(IOptionAliasValidator inputValidator, ICollectTillInputCorrect collectTillInputCorrect)
        {
            _inputValidator = inputValidator;
            _collectTillInputCorrect = collectTillInputCorrect;
        }

        public string Invoke(OptionToken optionToken)
        {
            var value = optionToken.Value;
            var splittedOption = value.TrimStart('-').Split('-');
            var suggestion = new string(splittedOption.Select(s => s.First()).ToArray());
            var optionAlias = _collectTillInputCorrect.CollectTillInputIsValid($"Please enter an alias for your option: '{value}' suggestion: '-{suggestion}'", _inputValidator);
            return optionAlias;
        }
    }
}
