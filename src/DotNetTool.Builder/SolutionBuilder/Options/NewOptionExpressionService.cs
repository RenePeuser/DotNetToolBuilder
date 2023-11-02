using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal sealed class NewOptionExpressionService : INewOptionExpressionService
    {
        private readonly IEnumerable<INewOptionExpressionBuilder> _newOptionExpressionBuilders;

        public NewOptionExpressionService(IEnumerable<INewOptionExpressionBuilder> newOptionExpressionBuilders)
        {
            Throw.IfNullOrEmpty(newOptionExpressionBuilders);

            _newOptionExpressionBuilders = newOptionExpressionBuilders;
        }

        public string Build(OptionInfo optionInfo)
        {
            Throw.IfNull(() => optionInfo);

            var builder = _newOptionExpressionBuilders.Single(b => b.IsBuilderFor(optionInfo));
            return builder.Build(optionInfo);
        }
    }
}
