using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public interface INewOptionExpressionBuilder
    {
        string Build(OptionInfo optionInfo);
        bool IsBuilderFor(OptionInfo optionInfo);
    }
}
