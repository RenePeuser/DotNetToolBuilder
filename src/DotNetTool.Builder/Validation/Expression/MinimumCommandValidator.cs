namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Argument.Check;
    using Extensions;
    using Models;
    using Tokenizer.Tokens;

    public class MinimumCommandValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CollectErrors(dotNetToolName, expressionInfo).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(string dotNetToolName, ExpressionInfo expression)
        {
            var commands = expression.Tokens.OfType<CommandToken>().ToList();
            if (commands.Count < 2)
            {
                yield return $"The expression must have minimum one command. Sample: '{dotNetToolName} myCommand'";
            }
        }
    }
}