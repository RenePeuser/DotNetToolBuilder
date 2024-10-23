using System.Linq;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class CollectOptionAlias(IOptionAliasValidator inputValidator,
                                             ICollectTillInputCorrect collectTillInputCorrect) : ICollectOptionAlias
    {
        public string Invoke(OptionToken optionToken)
        {
            var value = optionToken.Value;
            var splittedOption = value.TrimStart('-').Split('-');
            var suggestion = new string(splittedOption.Select(s => s.First()).ToArray());
            var optionAlias = collectTillInputCorrect.CollectTillInputIsValid($"Please enter an alias for your option: '{value}' suggestion: '-{suggestion}'", inputValidator);
            return optionAlias;
        }
    }
}
