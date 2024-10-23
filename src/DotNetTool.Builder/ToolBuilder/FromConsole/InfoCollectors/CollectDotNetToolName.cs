using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class CollectDotNetToolName(IToolNameValidator toolNameValidator,
                                                ICollectTillInputCorrect collectTillInputCorrect,
                                                IDotNetToolNameNormalizer dotNetToolNameNormalizer) : ICollectDotNetToolName
    {
        private const string Title = "Please enter the name of the DotNetTool: (Sample: 'myTool')";

        public DotNetToolName Collect(string projectName)
        {
            var dotNetToolName = collectTillInputCorrect.CollectTillInputIsValid(Title, projectName, toolNameValidator);
            var normalizedName = dotNetToolNameNormalizer.Normalize(dotNetToolName);
            return new DotNetToolName(dotNetToolName, normalizedName);
        }
    }
}
