namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ExpressionCommandMustBeforeOptionOrArgumentValidator : IExpressionContentValidator
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
            bool optionOrArgumentExists = false;
            foreach (var value in splittedExpression)
            {
                if (value.Contains("--") || value.Contains("<") || value.Contains("["))
                {
                    optionOrArgumentExists = true;
                }
                else if (optionOrArgumentExists)
                {
                    yield return $"Command: '{value}' was defined after argument or option.";
                }
            }
        }
    }
}
