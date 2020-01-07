using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services
{
    internal class CollectTillInputCorrect : ICollectTillInputCorrect
    {
        private readonly IConsoleService _consoleService;

        public CollectTillInputCorrect(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public string CollectTillUserInputOk(string messageForUser, params string[] expectedInput)
        {
            string required = string.Empty;
            while (required.NotEqualsAnyOf(expectedInput) || required.IsNullOrWhiteSpace())
            {
                _consoleService.WriteInput(messageForUser);
                required = _consoleService.ReadLine().Trim();
            }

            return required;
        }
    }
}