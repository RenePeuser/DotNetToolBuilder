using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Argument
{
    public interface IArgumentParser : IParameterValueParser
    {
        ArgumentInfo Parse(string value);
    }
}