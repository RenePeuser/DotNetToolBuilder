using DotNetTool.Builder.Models;
using DotNetTool.Builder.Tokenizer.Tokens;

namespace DotNetTool.Builder.Parser.Options
{
    internal interface IOptionParser
    {
        OptionInfo Parse(OptionToken token, ArgumentInfo argument);
    }
}
