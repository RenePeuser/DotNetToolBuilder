using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Parser.Parameters
{
    public class ParameterParser : IParameterParser
    {
        private readonly IConsoleService _consoleService;

        public ParameterParser(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }

        public bool IsThisParserFor(string value)
        {
            return !value.StartsWith("-") && !value.StartsWith("[") && !value.StartsWith("<");
        }

        public ParameterInfo Parse(string value, IEnumerable<OptionInfo> options)
        {
            var parameter = new ParameterInfo();
            parameter.Name = value;
            _consoleService.WriteInput($"Please enter a description for your command: '{parameter.Name}'");
            var description = _consoleService.ReadLine();
            parameter.Description = description;
            parameter.Options = options.ToList();

            return parameter;
        }

        public ParameterInfo Parse(string value, IEnumerable<OptionInfo> options, ParameterInfo parameterInfo)
        {
            var parameter = new ParameterInfo();
            parameter.Name = value;
            parameter.Description = parameterInfo.Description;
            parameter.Options = parameterInfo.Options.ToList();
            return parameter;
        }
    }
}
