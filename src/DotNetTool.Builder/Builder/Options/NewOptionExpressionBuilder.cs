using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public interface INewOptionExpressionService
    {
        string Build(OptionInfo optionInfo);
    }

    public class NewOptionExpressionService : INewOptionExpressionService
    {
        private readonly IEnumerable<INewOptionExpressionBuilder> _newOptionExpressionBuilders;

        public NewOptionExpressionService(IEnumerable<INewOptionExpressionBuilder> newOptionExpressionBuilders)
        {
            _newOptionExpressionBuilders = newOptionExpressionBuilders;
        }

        public string Build(OptionInfo optionInfo)
        {
            var builder = _newOptionExpressionBuilders.Single(builder => builder.IsBuilderFor(optionInfo));
            return builder.Build(optionInfo);
        }
    }
}