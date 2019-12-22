using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Parser.Parameters;
using DotNetTool.Builder.Services;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Parser
{
    public class ParameterExpressionParser : IParameterExpressionParser
    {
        private readonly IParameterService _parameterService;
        private readonly IEnumerable<IParameterValueParser> _parsers;

        public ParameterExpressionParser(IEnumerable<IParameterValueParser> parsers, IParameterService parameterService)
        {
            _parsers = parsers;
            _parameterService = parameterService;
        }

        public ParameterInfo Parse(string paramterExpression, ParameterInfo lastParameter)
        {
            var splittedExpression = paramterExpression.Split(" ");
            var options = new List<OptionInfo>();
            ParameterInfo lastParameterInfo = null;
            ArgumentInfo argument = null;

            for (var i = splittedExpression.Length - 1; i >= 0; i--)
            {
                var currentWithTypeInfo = splittedExpression[i];
                var current = currentWithTypeInfo;
                var foundParser = _parsers.SingleOrDefault(p => p.IsThisParserFor(currentWithTypeInfo));
                ParameterInfo parameter = null;

                switch (foundParser)
                {
                    case IArgumentParser argumentParser:
                        var alreadyExistingArgument =
                            _parameterService.FindAlreadyExistingArgument(current, lastParameter);
                        argument = alreadyExistingArgument.IsNull()
                            ? argumentParser.Parse(currentWithTypeInfo)
                            : alreadyExistingArgument;
                        break;
                    case IOptionParser optionParser:
                        var alreadyExistingOption = _parameterService.FindAlreadyExistingOption(current, lastParameter);
                        options.Add(alreadyExistingOption.IsNull()
                            ? optionParser.Parse(currentWithTypeInfo, argument)
                            : alreadyExistingOption);
                        break;
                    case IParameterParser parameterParser:
                        var commandAlreadyExists = _parameterService.FindAlreadyExistingCommand(current, lastParameter);
                        parameter = commandAlreadyExists.IsNull()
                            ? parameterParser.Parse(currentWithTypeInfo, options)
                            : parameterParser.Parse(current, options, commandAlreadyExists);
                        options = new List<OptionInfo>();
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (lastParameterInfo.IsNotNull()) parameter.SubCommands = lastParameterInfo.ToIList();

                if (parameter.IsNotNull())
                {
                    parameter.ArgumentInfo = argument;
                    if (argument.IsNotNull()) argument = null;
                }

                lastParameterInfo = parameter;
                if (lastParameter.IsNotNull())
                {
                    var parentForThis = _parameterService.FindAlreadyExistingCommand(current, lastParameter);
                    if (parentForThis.IsNotNull())
                        if (parentForThis.SubCommands.IsNotNull())
                            parentForThis.SubCommands = parentForThis.SubCommands.Concat(parameter.SubCommands);
                }
            }

            if (lastParameter.IsNotNull()) return lastParameter;

            return lastParameterInfo;
        }
    }
}