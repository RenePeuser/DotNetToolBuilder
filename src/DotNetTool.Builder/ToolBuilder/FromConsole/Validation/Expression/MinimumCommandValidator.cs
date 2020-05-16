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
    internal class MinimumCommandValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(dotNetDotNetToolName, expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(DotNetToolName dotNetDotNetToolName, ExpressionInfo expression)
        {
            var commands = expression.Tokens.OfType<CommandToken>().ToList();
            if (commands.Count < 2)
            {
                yield return $"The expression:'{expression.Expression}' must have minimum one command. Sample: '{dotNetDotNetToolName.Name} myCommand'";
            }
        }
    }
}
