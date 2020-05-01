using System;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ValidateArgumentInfo
    {
        private readonly ValidateTypeFromString _validateTypeFromString;
        private readonly ValidateArgumentNameFromString _validateArgumentNameFromString;

        public ValidateArgumentInfo(ValidateTypeFromString validateTypeFromString, ValidateArgumentNameFromString validateArgumentNameFromString)
        {
            _validateTypeFromString = validateTypeFromString;
            _validateArgumentNameFromString = validateArgumentNameFromString;
        }

        internal ArgumentInfoValidationResult Validate(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.IsNull())
            {
                return new ArgumentInfoValidationResult(argumentInfo, string.Empty);
            }

            var argumentTypeErrors = _validateTypeFromString.CollectErrors(argumentInfo.Type).ToList();
            var argumentNameErrors = _validateArgumentNameFromString.CheckForErrors(argumentInfo.Name);
            var allErrors = argumentTypeErrors.Concat(argumentNameErrors).FilterNullOrWhitespace().ToList();

            return new ArgumentInfoValidationResult(argumentInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);

        }
    }
}