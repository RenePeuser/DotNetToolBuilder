namespace DotNetTool.Builder.Builder.Options
{
    using Models;

    internal interface INewOptionExpressionService
    {
        string Build(OptionInfo optionInfo);
    }
}