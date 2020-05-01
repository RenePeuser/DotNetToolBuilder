using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using DotNetTool.Builder.ToolBuilder.FromConsole.Tokenizer.Tokens;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class DuplicatedCommandValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(ExpressionInfo expression)
        {
            var commands = expression.Tokens.OfType<CommandToken>().ToList();

            CommandToken lastCommandToken = null;
            foreach (var commandToken in commands)
            {
                if (lastCommandToken?.Value?.ToLower() == commandToken?.Value?.ToLower())
                {
                    yield return $"The command: '{commandToken.Value}', must not be equal to the previous one: '{lastCommandToken.Value}'";
                }

                lastCommandToken = commandToken;
            }
        }
    }
}
