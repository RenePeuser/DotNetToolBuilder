using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Parameters
{
    public interface IParameterParser : IParameterValueParser
    {
        CliParameterInfo Parse(string value, IEnumerable<OptionInfo> options);
        CliParameterInfo Parse(string value, IEnumerable<OptionInfo> options, CliParameterInfo parameterInfo);
    }
}