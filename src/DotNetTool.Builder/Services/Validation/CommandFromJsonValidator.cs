using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal class CommandFromJsonValidator : IDotNetToolFromJsonValidator
    {
        private readonly IEnumerable<ICommandInfoValidator> _validators;

        public CommandFromJsonValidator(IEnumerable<ICommandInfoValidator> validators)
        {
            _validators = validators;
        }

        public IEnumerable<ValidationResult> Validate(Models.DotNetTool dotNetTool)
        {
            return ValidateInternal(dotNetTool.ParameterInfo);
        }

        private IEnumerable<ValidationResult> ValidateInternal(CommandInfo parameterInfo)
        {
            var result = _validators.SelectMany(validator => validator.Validate(parameterInfo));
            foreach (var validationResult in result)
            {
                yield return validationResult;
            }

            foreach (var parameterInfoSubCommand in parameterInfo.SubCommands)
            {
                var subCommandResults = ValidateInternal(parameterInfoSubCommand);
                foreach (var validationResult in subCommandResults)
                {
                    yield return validationResult;
                }
            }
        }
    }
}