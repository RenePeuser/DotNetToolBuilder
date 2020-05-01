using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal interface ICollectOptionAlias
    {
        string Invoke(OptionToken optionToken);
    }
}
