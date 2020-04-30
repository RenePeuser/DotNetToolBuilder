using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class CommandNameValidator : ICommandInfoValidator
    {
        private readonly ValidateCommandNameFromString _validateCommandNameFromString;

        public CommandNameValidator(ValidateCommandNameFromString validateCommandNameFromString)
        {
            _validateCommandNameFromString = validateCommandNameFromString;
        }

        public IEnumerable<ValidationResult> Validate(CommandInfo commandInfo)
        {
            var errors = _validateCommandNameFromString.CollectErrors(commandInfo.Name).ToList();
            yield return new ValidationResult(errors.Flatten(Environment.NewLine));
        }
    }
}