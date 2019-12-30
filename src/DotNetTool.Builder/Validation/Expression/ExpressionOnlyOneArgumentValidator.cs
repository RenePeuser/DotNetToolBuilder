namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using Extensions;

    public class ExpressionOnlyOneArgumentValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errors = ValidateExpression(expression).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> ValidateExpression(string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                yield return "The expression must not be null, empty or whitespace";
            }

            var splittedValue = expression.Split();


            for (int i = 0; i < splittedValue.Length; i++)
            {
                var current = splittedValue[i];
                var previous = i > 0 ? splittedValue[i - 1] : null;

                if(previous.IsNull())
                {
                    continue;
                }

                if (current.Contains("<") || current.Contains(">"))
                {
                    if (previous.Contains("<") || previous.Contains(">"))
                    {
                        yield return $"Multiple arguments: {previous} {current} it is not allowed. For each command or option only one argument";
                    }
                }
            }
        }

        private bool IsValidInternal(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                return false;
            }

            var split = expression.Split(' ');
            return split.Length > 1;
        }
    }
}