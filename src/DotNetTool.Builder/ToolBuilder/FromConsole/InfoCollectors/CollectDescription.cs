using Argument.Check;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class CollectDescription(ICollectTillInputCorrect collectTillInputCorrect,
                                             IDescriptionValidator descriptionValidator) : ICollectDescription
    {
        public string Collect(string title)
        {
            Throw.IfNullOrWhiteSpace(title);

            var description = collectTillInputCorrect.CollectTillInputIsValid(title, descriptionValidator);
            return description;
        }
    }
}
