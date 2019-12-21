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
        private readonly IEnumerable<IParameterValueParser> _parsers;

        public ParameterExpressionParser(IEnumerable<IParameterValueParser> parsers)
        {
            _parsers = parsers;
        }

        public CliParameterInfo Parse(string paramterExpression, CliParameterInfo lastParameter)
        {
            var splittedExpression = paramterExpression.Split(" ");
            var options = new List<OptionInfo>();
            CliParameterInfo lastCliParameterInfo = null;
            ArgumentInfo argument = null;

            for (int i = splittedExpression.Length - 1; i >= 0; i--)
            {
                var currentWithTypeInfo = splittedExpression[i];
                string current = currentWithTypeInfo;
                var foundParser = _parsers.SingleOrDefault(p => p.IsThisParserFor(currentWithTypeInfo));
                CliParameterInfo parameter = null;

                switch (foundParser)
                {
                    case IArgumentParser argumentParser:
                        var alreadyExistingArgument = CliParameterService.FindAlreadyExistingArgument(current, lastParameter);
                        argument = alreadyExistingArgument.IsNull() ? argumentParser.Parse(currentWithTypeInfo) : alreadyExistingArgument;
                        break;
                    case IOptionParser optionParser:
                        var alreadyExistingOption = CliParameterService.FindAlreadyExistingOption(current, lastParameter);
                        options.Add(alreadyExistingOption.IsNull() ? optionParser.Parse(currentWithTypeInfo, argument) : alreadyExistingOption);
                        break;
                    case IParameterParser parameterParser:
                        var commandAlreadyExists = CliParameterService.FindAlreadyExistingCommand(current, lastParameter);
                        parameter = commandAlreadyExists.IsNull() ? parameterParser.Parse(currentWithTypeInfo, options) : parameterParser.Parse(current, options, commandAlreadyExists);
                        options = new List<OptionInfo>();
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (lastCliParameterInfo.IsNotNull())
                {
                    parameter.SubCommands = lastCliParameterInfo.ToIList();
                }

                if (parameter.IsNotNull())
                {
                    parameter.ArgumentInfo = argument;
                    if (argument.IsNotNull())
                    {
                        argument = null;
                    }
                }
                
                lastCliParameterInfo = parameter;
                if (lastParameter.IsNotNull())
                {
                    var parentForThis = CliParameterService.FindAlreadyExistingCommand(current, lastParameter);
                    if (parentForThis.IsNotNull())
                    {
                        if (parentForThis.SubCommands.IsNotNull())
                        {
                            parentForThis.SubCommands = parentForThis.SubCommands.Concat(parameter.SubCommands);
                        }
                    }
                }
            }

            if (lastParameter.IsNotNull())
            {
                return lastParameter;
            }

            return lastCliParameterInfo;
        }
    }
}