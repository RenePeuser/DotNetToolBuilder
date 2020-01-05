using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Options
{
    using Tokenizer.Tokens;

    internal interface IOptionParser
    {
        OptionInfo Parse(OptionToken token, ArgumentInfo argument);
    }
}
