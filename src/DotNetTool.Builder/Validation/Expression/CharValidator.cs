using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class CharValidator : IExpressionContentValidator
    {
        private const string ValidationInfo = "Only letters, digits, '[', ']', '<', '>' and '-' allowed";

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

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            return new ValidationResult(IsValidInternal(expression), ValidationInfo);
        }

        private bool IsValidInternal(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            return value.All(c => _validationRules.Any(validation => validation(c)));
        }
    }
}