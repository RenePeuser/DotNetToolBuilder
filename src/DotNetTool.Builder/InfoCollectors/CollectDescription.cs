using Argument.Check;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class CollectDescription : ICollectDescription
    {
        private readonly ICollectTillInputCorrect _collectTillInputCorrect;
        private readonly IDescriptionValidator _descriptionValidator;

        public CollectDescription(ICollectTillInputCorrect collectTillInputCorrect, IDescriptionValidator descriptionValidator)
        {
            _collectTillInputCorrect = collectTillInputCorrect;
            _descriptionValidator = descriptionValidator;
        }

        public string Collect(string title)
        {
            Throw.IfNullOrWhiteSpace(() => title);

            var description = _collectTillInputCorrect.CollectTillInputIsValid(title, _descriptionValidator);
            return description;
        }
    }
}
