using System;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateOptionInfo(ValidateArgumentInfo validateArgumentInfo,
                                             IOptionAliasValidator optionAliasValidator,
                                             ValidateOptionFromString validateOptionFromString)
    {
        internal OptionInfoValidationResult Validate(OptionInfo optionInfo)
        {
            var argumentValidationResult = validateArgumentInfo.Validate(optionInfo.Argument);
            var optionAliasError = optionAliasValidator.Validate(optionInfo.Alias);
            var optionErrors = validateOptionFromString.CollectErrors(optionInfo.Value).ToList();

            var allErrors = optionErrors.Concat(optionAliasError.Errors).Concat(argumentValidationResult.Errors).FilterNullOrWhitespace().ToList();

            return new OptionInfoValidationResult(optionInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);
        }
    }
}