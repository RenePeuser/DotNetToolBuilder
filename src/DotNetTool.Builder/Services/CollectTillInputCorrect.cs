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

        public string CollectTillInputIsValid(string messageForUser, params string[] expectedInput)
        {
            Throw.IfNullOrWhiteSpace(() => messageForUser);
            Throw.IfNull(() => expectedInput);

            return CollectTillInputIsValid(messageForUser, input => input.ContainsAnyOf(expectedInput));
        }

        public string CollectTillInputIsValid(string messageForUser, Predicate<string> inputValidation)
        {
            Throw.IfNullOrWhiteSpace(() => messageForUser);
            Throw.IfNull(() => inputValidation);

            bool isValid = false;
            string description = string.Empty;
            while (isValid.IsFalse())
            {
                _consoleService.WriteInput(messageForUser);
                description = _consoleService.ReadLine().Trim();
                isValid = inputValidation(description);
            }

            return description;
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
                validationResult = inputValidator.IsValid(input);
                if (validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteError(validationResult.Errors);
                }
            }

            return input;
        }
    }
}