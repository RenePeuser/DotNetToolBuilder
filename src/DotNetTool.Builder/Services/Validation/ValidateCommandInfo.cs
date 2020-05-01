using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ValidateCommandInfo
    {
        private readonly ValidateArgumentInfo _validateArgumentInfo;
        private readonly ValidateOptionInfo _validateOptionInfo;
        private readonly ValidateCommandNameFromString _validateCommandNameFromString;

        public ValidateCommandInfo(ValidateArgumentInfo validateArgumentInfo, ValidateOptionInfo validateOptionInfo, ValidateCommandNameFromString validateCommandNameFromString)
        {
            _validateArgumentInfo = validateArgumentInfo;
            _validateOptionInfo = validateOptionInfo;
            _validateCommandNameFromString = validateCommandNameFromString;
        }

        internal IEnumerable<CommandInfoValidationResult> Validate(CommandInfo commandInfo)
        {
            yield return ValidateCommandInfoInternal(commandInfo);

            foreach (var subCommand in commandInfo.SubCommands)
            {
                var validationResults = Validate(subCommand);
                foreach (var result in validationResults)
                {
                    yield return result;
                }
            }
        }

        private CommandInfoValidationResult ValidateCommandInfoInternal(CommandInfo commandInfo)
        {
            var argumentValidationResult = _validateArgumentInfo.Validate(commandInfo.Argument);
            var optionsValidationResults = commandInfo.Options.Select(option => _validateOptionInfo.Validate(option)).Select(result => result.Errors).ToList();
            var commandNameValidationResult = _validateCommandNameFromString.CollectErrors(commandInfo.Name).ToList();

            var allErrors = argumentValidationResult.Errors.Concat(optionsValidationResults).Concat(commandNameValidationResult).FilterNullOrWhitespace().ToList();

            return new CommandInfoValidationResult(commandInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);
        }
    }
}