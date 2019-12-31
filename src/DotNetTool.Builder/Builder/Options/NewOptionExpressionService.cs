using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    using global::Argument.Check;

    public class NewOptionExpressionService : INewOptionExpressionService
    {
        private readonly IEnumerable<INewOptionExpressionBuilder> _newOptionExpressionBuilders;

        public NewOptionExpressionService(IEnumerable<INewOptionExpressionBuilder> newOptionExpressionBuilders)
        {
            Throw.IfNullOrEmpty(() => newOptionExpressionBuilders);

            _newOptionExpressionBuilders = newOptionExpressionBuilders;
        }

        public string Build(OptionInfo optionInfo)
        {
            Throw.IfNull(() => optionInfo);

            var builder = _newOptionExpressionBuilders.Single(builder => builder.IsBuilderFor(optionInfo));
            return builder.Build(optionInfo);
        }
    }
}
