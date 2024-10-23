using System;
using Argument.Check;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.Services.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Services
{
    internal sealed class CollectTillInputCorrect(IConsoleService consoleService) : ICollectTillInputCorrect
    {
        public string CollectTillInputIsValid(string messageForUser, string projectName, IToolNameValidator toolNameValidator)
        {

            Throw.IfNullOrWhiteSpace(messageForUser);
            Throw.IfNull(() => toolNameValidator);

            string input = null;
            ValidationResult validationResult = null;
            while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
            {
                consoleService.WriteInput(messageForUser);
                input = consoleService.ReadLine().Trim();
                validationResult = toolNameValidator.Validate(input, projectName);
                if (validationResult.IsValid.IsFalse())
                {
                    consoleService.WriteError(validationResult.Errors);
                    consoleService.WriteLine();
                }
            }

            return input;
        }

        public string CollectTillInputIsValid(string messageForUser, Predicate<string> inputValidation, Func<string, string> getErrorMessageForInput)
        {
            Throw.IfNullOrWhiteSpace(messageForUser);
            Throw.IfNull(() => inputValidation);

            var isValid = false;
            var input = string.Empty;
            while (isValid.IsFalse())
            {
                consoleService.WriteInput(messageForUser);
                input = consoleService.ReadLine().Trim();
                isValid = inputValidation(input);
                if (isValid.IsFalse())
                {
                    consoleService.WriteError(getErrorMessageForInput(input));
                    consoleService.WriteLine();
                }
            }

            return input;
        }

        public string CollectTillInputIsValid(string messageForUser, IInputValidator inputValidator)
        {
            Throw.IfNullOrWhiteSpace(messageForUser);
            Throw.IfNull(() => inputValidator);

            string input = null;
            ValidationResult validationResult = null;
            while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
            {
                consoleService.WriteInput(messageForUser);
                input = consoleService.ReadLine().Trim();
                validationResult = inputValidator.Validate(input);
                if (validationResult.IsValid.IsFalse())
                {
                    consoleService.WriteError(validationResult.Errors);
                    consoleService.WriteLine();
                }
            }

            return input;
        }
    }
}
