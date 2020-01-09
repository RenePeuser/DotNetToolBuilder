using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectDotNetToolName : ICollectDotNetToolName
    {
        private const string Title = "Please enter the name of the DotNetTool: (Sample: 'myTool')";
        private readonly IToolNameValidator _inputValidator;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

        public CollectDotNetToolName(IToolNameValidator inputValidator, ICollectTillInputCorrect collectTillInputCorrect)
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
