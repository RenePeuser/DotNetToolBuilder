using Argument.Check;
using DotNetTool.Builder.Models;
using Extensions.Pack;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal sealed class NewOptionExpressionBuilderWithoutArgument : INewOptionExpressionBuilder
    {
        private const string OptionTemplate =
            @"Option([""$option-name$"", ""$option-alias$""], ""$option-description$"")
            {
                Required = $required-value$
            }";

        public string Build(OptionInfo optionInfo)
        {
            Throw.IfNull(() => optionInfo);

            var newTemplate = OptionTemplate.Replace("$option-name$", optionInfo.Value)
                                            .Replace("$option-alias$", optionInfo.Alias)
                                            .Replace("$option-description$", optionInfo.Description)
                                            .Replace("$required-value$", optionInfo.IsIsRequired.ToString().ToLower());

            return newTemplate;
        }

        public bool IsBuilderFor(OptionInfo optionInfo)
        {
            Throw.IfNull(() => optionInfo);

            return optionInfo.Argument.IsNull();
        }
    }
}
