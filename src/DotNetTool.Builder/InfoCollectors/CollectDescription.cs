using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectDescription : ICollectDescription
    {
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;

        public CollectDescription(ICollectTillInputCorrect collectTillInputCorrect)
        {
            _collectTillInputCorrect = collectTillInputCorrect;
        }
        public string Collect(string title)
        {
            Throw.IfNullOrWhiteSpace(() => title);

            var description = _collectTillInputCorrect.CollectTillInputIsValid(title, input => input.IsNotNullOrWhiteSpace());
            return description;
        }
    }
}