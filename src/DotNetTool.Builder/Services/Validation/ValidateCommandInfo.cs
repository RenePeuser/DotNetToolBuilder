using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateCommandInfo(ValidateArgumentInfo validateArgumentInfo,
                                              ValidateOptionInfo validateOptionInfo,
                                              ValidateCommandNameFromString validateCommandNameFromString)
    {
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
            var argumentValidationResult = validateArgumentInfo.Validate(commandInfo.Argument);
            var optionsValidationResults = commandInfo.Options.Select(option => validateOptionInfo.Validate(option)).Select(result => result.Errors).ToList();
            var commandNameValidationResult = validateCommandNameFromString.CollectErrors(commandInfo.NormalizedName).ToList();

            var allErrors = argumentValidationResult.Errors.Concat(optionsValidationResults).Concat(commandNameValidationResult).FilterNullOrWhitespace().ToList();

            return new CommandInfoValidationResult(commandInfo, allErrors.Any() ? allErrors.Flatten(Environment.NewLine) : string.Empty);
        }
    }
}