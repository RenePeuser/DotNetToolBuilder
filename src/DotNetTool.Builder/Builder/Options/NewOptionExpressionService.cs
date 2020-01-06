using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    internal class NewOptionExpressionService : INewOptionExpressionService
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
