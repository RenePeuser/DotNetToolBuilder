using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectDotNetToolName : ICollectDotNetToolName
    {
        private const string Title = "Please enter the name of the DotNetTool: (Sample: 'myTool')";
        private readonly IToolNameValidator _inputValidator;
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public CollectDotNetToolName(IToolNameValidator inputValidator, ICollectTillInputCorrect collectTillInputCorrect, IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            _inputValidator = inputValidator;
            _collectTillInputCorrect = collectTillInputCorrect;
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public DotNetToolName Collect()
        {
            var dotNetToolName = _collectTillInputCorrect.CollectTillInputIsValid(Title, _inputValidator);
            var normalizedName = _dotNetToolNameNormalizer.Normalize(dotNetToolName);
            return new DotNetToolName(dotNetToolName, normalizedName);
        }
    }
}
