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

        public CommandInfo Parse(ExpressionInfo parameterExpression, CommandInfo previousCommand)
        {
            IList<OptionInfo> options = new List<OptionInfo>();
            CommandInfo lastCommand = null;
            ArgumentInfo lastArgument = null;

            foreach (var token in parameterExpression.Tokens.Reverse())
            {
                switch (token)
                {
                    case ArgumentToken argumentToken:
                        var currentArgument = _argumentParser.Parse(argumentToken);
                        var existingArgument = _parameterService.FindAlreadyExistingArgument(currentArgument, previousCommand);
                        lastArgument = existingArgument.IsNotNull() ? existingArgument : currentArgument;
                        break;
                    case OptionToken optionToken:
                        var currentOption = _optionParser.Parse(optionToken, lastArgument);
                        options.Add(currentOption);
                        lastArgument = null;
                        break;
                    case CommandToken commandToken:
                        var optionsInCorrectOrder = options.Reverse().ToList();
                        var existingCommand = _parameterService.FindAlreadyExistingCommand(commandToken, previousCommand);
                        lastCommand = _commandParser.Parse(commandToken, lastArgument, optionsInCorrectOrder, lastCommand, existingCommand, previousCommand);
                        options = new List<OptionInfo>();
                        lastArgument = null;
                        break;
                    default:
                        throw new InvalidOperationException($"Parameter expression: {parameterExpression.OptimizedExpressions} has invalid tokens, please check validation logic.");
                }
            }

            return lastCommand;
        }
    }
}
