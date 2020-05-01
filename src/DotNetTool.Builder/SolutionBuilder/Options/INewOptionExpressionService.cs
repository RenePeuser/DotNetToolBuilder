using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal interface INewOptionExpressionService
    {
        string Build(OptionInfo optionInfo);
    }
}
