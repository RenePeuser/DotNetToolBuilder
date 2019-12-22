using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Parameters
{
    public interface IParameterParser : IParameterValueParser
    {
        ParameterInfo Parse(string value, IEnumerable<OptionInfo> options);
        ParameterInfo Parse(string value, IEnumerable<OptionInfo> options, ParameterInfo parameterInfo);
    }
}
