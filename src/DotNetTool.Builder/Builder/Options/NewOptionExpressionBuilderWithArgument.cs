using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public class NewOptionExpressionBuilderWithArgument : INewOptionExpressionBuilder
    {
        private const string OptionArgumentTemplate =
            @"Option(new[] { ""$option-name$"", ""$option-alias$"" }, ""$option-description$"".AsDescription())
{
    Required = $required-value$,
    Argument = new Argument(""$option-argument-name$"")
}";

        public string Build(OptionInfo optionInfo)
        {
            var newTemplate = OptionArgumentTemplate.Replace("$option-name$", optionInfo.Name)
                .Replace("$option-alias$", optionInfo.Alias)
                .Replace("$option-argument-name$", optionInfo.Argument.Name)
                .Replace("$option-description$", optionInfo.Description)
                .Replace("$required-value$", optionInfo.Required.ToString().ToLower());

            return newTemplate;
        }

        public bool IsBuilderFor(OptionInfo optionInfo)
        {
            return optionInfo.Argument.IsNotNull();
        }
    }
}
