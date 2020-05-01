using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal class CollectDotNetToolName : ICollectDotNetToolName
    {
        private const string Title = "Please enter the name of the DotNetTool: (Sample: 'myTool')";
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;
        private readonly IToolNameValidator _inputValidator;

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
