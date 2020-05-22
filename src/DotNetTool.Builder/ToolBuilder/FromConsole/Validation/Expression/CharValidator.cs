using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal class CharValidator : IExpressionContentValidator
    {
        private readonly IEnumerable<Predicate<char>> _validationRules = new Predicate<char>[] { char.IsLetterOrDigit, char.IsWhiteSpace, c => c == '[', c => c == ']', c => c == '<', c => c == '>', c => c == '-', c => c == '.' };

        public ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo,
            string projectName)
        {
            Throw.IfNull(() => dotNetDotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CheckForErrors(expressionInfo).ToList();
            return new ValidationResult(errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CheckForErrors(ExpressionInfo expression)
        {
            var expressionValue = expression.Expression;
            if (expressionValue.IsNullOrWhiteSpace())
            {
                yield break;
            }

            if (expressionValue.All(c => _validationRules.Any(validation => validation(c))).IsFalse())
            {
                yield return $"The expression: '{expression.Expression}' must only contains letters, digits, '[', ']', '<', '>' and '-' allowed";
            }
        }
    }
}
