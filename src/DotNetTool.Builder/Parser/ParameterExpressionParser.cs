using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Parser
{
    using Commands;
    using global::Argument.Check;
    using Tokenizer.Tokens;

    public class ParameterExpressionParser : IParameterExpressionParser
    {
        private readonly ICommandParser _commandParser;
        private readonly IArgumentParser _argumentParser;
        private readonly IOptionParser _optionParser;
        private readonly IParameterService _parameterService;

        public ParameterExpressionParser(ICommandParser commandParser, IArgumentParser argumentParser, IOptionParser optionParser, IParameterService parameterService)
        {
            Throw.IfNull(() => commandParser);
            Throw.IfNull(() => argumentParser);
            Throw.IfNull(() => optionParser);
            Throw.IfNull(() => parameterService);

            _commandParser = commandParser;
            _argumentParser = argumentParser;
            _optionParser = optionParser;
            _parameterService = parameterService;
        }

        public CommandInfo Parse(ExpressionInfo parameterExpression, CommandInfo lastParameter)
        {
            var options = new List<OptionInfo>();
            CommandInfo lastCommand = null;
            ArgumentInfo argument = null;

            foreach (var token in parameterExpression.Tokens.Reverse())
            {
                CommandInfo parameter = null;
                switch (token)
                {
                    case ArgumentToken argumentToken:
                        var currentArgument = _argumentParser.Parse(argumentToken);
                        var existingArgument = _parameterService.FindAlreadyExistingArgument(currentArgument, lastParameter);
                        argument = existingArgument.IsNotNull() ? existingArgument : currentArgument;
                        break;
                    case OptionToken optionToken:
                        var currentOption = _optionParser.Parse(optionToken, argument);
                        var existingOption = _parameterService.FindAlreadyExistingOption(currentOption, lastParameter);
                        options.Add(existingOption.IsNotNull() ? existingOption : currentOption);
                        break;
                    case CommandToken commandToken:
                        var currentCommand = _commandParser.Parse(commandToken, options);
                        var existingCommand = _parameterService.FindAlreadyExistingCommand(currentCommand, lastParameter);
                        parameter = existingCommand.IsNotNull() ? existingCommand : _commandParser.Parse(commandToken, options, currentCommand);
                        options = new List<OptionInfo>();
                        break;
                    default:
                        throw new InvalidOperationException($"Parameter expression: {parameterExpression.OptimizedExpressions} has invalid tokens, please check validation logic.");
                }

                if (lastCommand.IsNotNull())
                {
                    parameter.SubCommands = lastCommand.ToIList();
                }

                if (parameter.IsNotNull())
                {
                    parameter.ArgumentInfo = argument;
                    if (argument.IsNotNull())
                    {
                        argument = null;
                    }
                }

                lastCommand = parameter;
                if (lastParameter.IsNotNull())
                {
                    var parentForThis = _parameterService.FindAlreadyExistingCommand(parameter, lastParameter);
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

            return lastCommand;
        }
    }
}
