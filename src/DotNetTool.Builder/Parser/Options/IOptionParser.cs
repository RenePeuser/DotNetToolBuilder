using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Options
{
    public interface IOptionParser : IParameterValueParser
    {
        OptionInfo Parse(string value, ArgumentInfo argument);
    }
}
