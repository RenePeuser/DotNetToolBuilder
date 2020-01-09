using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectProjectName : ICollectProjectName
    {
        private const string Title = "Please enter the name of your project: (Sample: 'My.New.Tool', this is the name of your solution !)";

        private readonly IProjectNameValidator _inputValidator;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

        public CollectProjectName(IProjectNameValidator inputValidator, ICollectTillInputCorrect collectTillInputCorrect)
        {
            _inputValidator = inputValidator;
            _collectTillInputCorrect = collectTillInputCorrect;
        }

        public string Invoke()
        {
            var input = _collectTillInputCorrect.CollectTillInoutIsValid(Title, _inputValidator);
            return input;
        }
    }
}
