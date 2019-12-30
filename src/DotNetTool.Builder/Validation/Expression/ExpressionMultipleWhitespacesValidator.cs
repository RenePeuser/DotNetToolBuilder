namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class ExpressionMultipleWhitespacesValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errros = Validate(dotNetToolName, expression).ToList();
            return new ValidationResult(errros.IsNullOrEmpty(), errros.Flatten(Environment.NewLine));
        }

        private IEnumerable<string> Validate(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                yield return "Expression must not be null or empty.";
                yield break;
            }

            var splittedValue = expression.Split().Distinct().ToArray();
            if (splittedValue.Length <= 1)
            {
                yield break;
            }

            for (int i = 1; i < splittedValue.Length; i++)
            {
                var current = splittedValue[i];
                if (current.IsNullOrWhiteSpace())
                {
                    yield return $"Multiple whitespace after: '{splittedValue[i - 1]}' please use only one whitespace as separator";
                }

            }
        }
    }
}