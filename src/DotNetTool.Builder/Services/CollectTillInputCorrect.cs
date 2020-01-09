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

        public string CollectTillInoutIsValid(string messageForUser, params string[] expectedInput)
        {
            string required = string.Empty;
            while (required.NotEqualsAnyOf(expectedInput) || required.IsNullOrWhiteSpace())
            {
                _consoleService.WriteInput(messageForUser);
                required = _consoleService.ReadLine().Trim();
            }

            return required;
        }

        public string CollectTillInoutIsValid(string messageForUser, IInputValidator inputValidator)
        {
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