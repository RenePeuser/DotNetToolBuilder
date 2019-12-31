using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    public abstract class CollectInfoStep : ICollectInfo
    {
        private readonly IConsoleService _consoleService;
        private readonly IInputValidator _inputValidator;

        protected CollectInfoStep(IConsoleService consoleService, IInputValidator inputValidator, string title)
        {
            _consoleService = consoleService;
            _inputValidator = inputValidator;
            Title = title;
        }

        public string Title { get; }

        public virtual string Invoke()
        {
            string input = null;
            ValidationResult validationResult = null;
            while (validationResult.IsNull() || validationResult.IsValid.IsFalse())
            {
                _consoleService.WriteInput(Title);
                input = _consoleService.ReadLine().Trim();
                validationResult = _inputValidator.IsValid(input);
                if (validationResult.IsValid.IsFalse())
                {
                    _consoleService.WriteError(validationResult.Errors);
                }
            }

            return input;
        }
    }
}
