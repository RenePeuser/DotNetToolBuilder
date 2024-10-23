using System;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateArgumentInfo(ValidateTypeFromString validateTypeFromString,
                                               ValidateArgumentNameFromString validateArgumentNameFromString)
    {
        internal ArgumentInfoValidationResult Validate(ArgumentInfo argumentInfo)
        {
            if (argumentInfo.IsNull())
            {
                return new ArgumentInfoValidationResult(argumentInfo, string.Empty);
            }

            var argumentTypeErrors = validateTypeFromString.CollectErrors(argumentInfo.Type).ToList();
            var argumentNameErrors = validateArgumentNameFromString.CheckForErrors(argumentInfo.Name);
            var allErrors = argumentTypeErrors.Concat(argumentNameErrors).FilterNullOrWhitespace().ToList();

            return new ArgumentInfoValidationResult(argumentInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);

        }
    }
}