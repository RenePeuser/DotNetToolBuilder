using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public class NewOptionExpressionBuilderWithoutArgument : INewOptionExpressionBuilder
    {
        private const string OptionTemplate =
            @"Option(new[] { ""$option-name$"", ""$option-alias$"" }, ""$option-description$"")
{
    Required = $required-value$
}";

        public string Build(OptionInfo optionInfo)
        {
            var newTemplate = OptionTemplate.Replace("$option-name$", optionInfo.Name)
                .Replace("$option-alias$", optionInfo.Alias)
                .Replace("$option-description$", optionInfo.Description)
                .Replace("$required-value$", optionInfo.Required.ToString().ToLower());

            return newTemplate;
        }

        public bool IsBuilderFor(OptionInfo optionInfo)
        {
            return optionInfo.Argument.IsNull();
        }
    }
}
