using System.Linq;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Tokenizer.Tokens;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectOptionAlias : ICollectOptionAlias
    {
        private readonly IOptionAliasValidator _inputValidator;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

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
            var input = _collectTillInputCorrect.CollectTillInoutIsValid($"Please enter an alias for your option: '{value}' suggestion: '-{suggestion}'", _inputValidator);
            return input;
        }
    }
}