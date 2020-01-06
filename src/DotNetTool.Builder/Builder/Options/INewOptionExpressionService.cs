using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    internal interface INewOptionExpressionService
    {
        string Build(OptionInfo optionInfo);
    }
}
