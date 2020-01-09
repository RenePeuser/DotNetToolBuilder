using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.InfoCollectors
{
    internal interface ICollectOptionAlias
    {
        string Invoke(OptionToken optionToken);
    }
}