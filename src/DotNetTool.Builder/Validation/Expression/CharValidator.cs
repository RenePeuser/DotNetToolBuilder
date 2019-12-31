using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Validation.Expression
{
    using Argument.Check;
    using Extensions;
    using Models;

    public class CharValidator : IExpressionContentValidator
    {
        private readonly IEnumerable<Predicate<char>> _validationRules = new Predicate<char>[]
        {
            char.IsLetterOrDigit,
            char.IsWhiteSpace,
            c => c == '[',
            c => c == ']',
            c => c == '<',
            c => c == '>',
            c => c == '-',
            c => c == '.',
        };

        public ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo)
        {
            Throw.IfNullOrWhiteSpace(() => dotNetToolName);
            Throw.IfNull(() => expressionInfo);

            var errors = CheckForErrors(expressionInfo).ToList();
            return new ValidationResult(errors.IsEmpty(), errors.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> CheckForErrors(ExpressionInfo expression)
        {
            var expressionValue = expression.Expression;
            if (expressionValue.IsNullOrWhiteSpace())
            {
                yield break;
            }

            if(expressionValue.All(c => _validationRules.Any(validation => validation(c))).IsFalse())
            {
                yield return "Only letters, digits, '[', ']', '<', '>' and '-' allowed";
            }
        }
    }
}