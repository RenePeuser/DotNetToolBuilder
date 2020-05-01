using System;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ValidateOptionInfo
    {
        private readonly ValidateArgumentInfo _validateArgumentInfo;
        private readonly IOptionAliasValidator _optionAliasValidator;
        private readonly ValidateOptionFromString _validateOptionFromString;

        public ValidateOptionInfo(ValidateArgumentInfo validateArgumentInfo, IOptionAliasValidator optionAliasValidator, ValidateOptionFromString validateOptionFromString)
        {
            _validateArgumentInfo = validateArgumentInfo;
            _optionAliasValidator = optionAliasValidator;
            _validateOptionFromString = validateOptionFromString;
        }

        internal OptionInfoValidationResult Validate(OptionInfo optionInfo)
        {
            var argumentValidationResult = _validateArgumentInfo.Validate(optionInfo.Argument);
            var optionAliasError = _optionAliasValidator.Validate(optionInfo.Alias);
            var optionErrors = _validateOptionFromString.CollectErrors(optionInfo.Value).ToList();

            var allErrors = optionErrors.Concat(optionAliasError.Errors).Concat(argumentValidationResult.Errors).FilterNullOrWhitespace().ToList();

            return new OptionInfoValidationResult(optionInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);
        }
    }
}