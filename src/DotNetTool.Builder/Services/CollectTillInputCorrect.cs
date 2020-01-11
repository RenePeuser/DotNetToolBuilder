using System;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services
{
    internal class CollectTillInputCorrect : ICollectTillInputCorrect
    {
        private readonly IConsoleService _consoleService;

        public CollectTillInputCorrect(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public string CollectTillInputIsValid(string messageForUser, Predicate<string> inputValidation, Func<string, string> getErrorMessageForInput)
        {
            Throw.IfNullOrWhiteSpace(() => messageForUser);
            Throw.IfNull(() => inputValidation);

            var isValid = false;
            var input = string.Empty;
            while (isValid.IsFalse())
            {
                _consoleService.WriteInput(messageForUser);
                input = _consoleService.ReadLine().Trim();
                isValid = inputValidation(input);
                if (isValid.IsFalse())
                {
                    _consoleService.WriteError(getErrorMessageForInput(input));
                }
            }

            return input;
        }

        public string CollectTillInputIsValid(string messageForUser, IInputValidator inputValidator)
        {
            Throw.IfNullOrWhiteSpace(() => messageForUser);
            Throw.IfNull(() => inputValidator);

            string input = null;
            ValidationResult validationResult = null;
            while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
            {
                _consoleService.WriteInput(messageForUser);
                input = _consoleService.ReadLine().Trim();
                validationResult = inputValidator.Validate(input);
                if (validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteError(validationResult.Errors);
                }
            }

            return input;
        }
    }
}
