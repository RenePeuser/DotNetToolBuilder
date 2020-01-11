using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectProjectName : ICollectProjectName
    {
        private const string Title = "Please enter the name of your project: (Sample: 'My.New.Tool', this is the name of your solution !)";
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

        private readonly IProjectNameValidator _inputValidator;

        public CollectProjectName(IProjectNameValidator inputValidator, ICollectTillInputCorrect collectTillInputCorrect)
        {
            _inputValidator = inputValidator;
            _collectTillInputCorrect = collectTillInputCorrect;
        }

        public string Collect()
        {
            var projectName = _collectTillInputCorrect.CollectTillInputIsValid(Title, _inputValidator);
            return projectName;
        }
    }
}
