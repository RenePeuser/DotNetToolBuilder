namespace DotNetTool.Builder.Builder.Options
{
    using Models;

    public interface INewOptionExpressionService
    {
        string Build(OptionInfo optionInfo);
    }
}