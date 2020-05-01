using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal interface INewOptionExpressionBuilder
    {
        string Build(OptionInfo optionInfo);
        bool IsBuilderFor(OptionInfo optionInfo);
    }
}
