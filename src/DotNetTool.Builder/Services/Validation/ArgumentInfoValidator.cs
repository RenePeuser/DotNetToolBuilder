using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ArgumentInfoValidator : ICommandInfoValidator
    {
        private readonly ValidateArgumentNameFromString _validateArgumentNameFromString;
        private readonly ValidateTypeFromString _validateTypeFromString;

        public ArgumentInfoValidator(ValidateArgumentNameFromString validateArgumentNameFromString, ValidateTypeFromString validateTypeFromString)
        {
            _validateArgumentNameFromString = validateArgumentNameFromString;
            _validateTypeFromString = validateTypeFromString;
        }

        public IEnumerable<ValidationResult> Validate(CommandInfo commandInfo)
        {
            var argument = commandInfo.Argument;
            if (argument.IsNull())
            {
                yield break;
            }

            var result = _validateArgumentNameFromString.CheckForErrors(argument.Name).Concat(_validateTypeFromString.CollectErrors(argument.Type));
            yield return new ValidationResult(result.Flatten(Environment.NewLine));
        }
    }
}