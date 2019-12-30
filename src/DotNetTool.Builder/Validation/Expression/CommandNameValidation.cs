namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class CommandNameValidation : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errors = CollectErrors(expression).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsNullOrWhiteSpace(), errors);
        }

        private IEnumerable<string> CollectErrors(string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                yield return "The expression must not be null, empty or whitespace";
            }

            var splittedExpression = expression.Split();
            foreach (var value in splittedExpression)
            {
                if (value.IsNullOrWhiteSpace())
                {
                    continue;
                }

                if (value.StartsWith("--").IsFalse() && value.StartsWith("<").IsFalse() && value.StartsWith("[").IsFalse())
                {
                    if (value.Contains("--") || value.Contains("<") || value.Contains("["))
                    {
                        yield return $"Command: {value} most not contain argument-, typecast- or option-syntax.";
                    }
                }
            }
        }
    }
}