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

        public CliParameterInfo Parse(string value, IEnumerable<OptionInfo> options)
        {
            var parameter = new CliParameterInfo();

            parameter.Name = value;
            _consoleService.WriteLine();
            _consoleService.WriteLine($"Please enter a description for your command: '{parameter.Name}'".AsInput());
            var description = _consoleService.ReadLine();
            parameter.Decsription = description;
            parameter.Options = options.ToList();

            return parameter;
        }

        public CliParameterInfo Parse(string value, IEnumerable<OptionInfo> options, CliParameterInfo parameterInfo)
        {
            var parameter = new CliParameterInfo();
            parameter.Name = value;
            parameter.Decsription = parameterInfo.Decsription;
            parameter.Options = parameterInfo.Options.ToList();
            return parameter;
        }
    }
}