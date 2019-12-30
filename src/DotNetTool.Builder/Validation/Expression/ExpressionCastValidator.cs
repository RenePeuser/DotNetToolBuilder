using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;

    public class ExpressionCastValidator : IExpressionContentValidator
    {
        private const string ValidationInfo = "Cast expressions must be open with '[' and closed with ']'";

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

            var splittedExpression = expression.Split(" ");
            foreach (var value in splittedExpression)
            {
                if (value.Contains("[") || value.Contains("]"))
                {
                    if (value.Count(c => c == '[' || c == ']') != 2 || value.IndexOf('[') > value.IndexOf(']'))
                    {
                        yield return "A type cast must begin with '[' and ends with ']'";
                    }

                    if (value.StartsWith("[") && value.EndsWith("]"))
                    {
                        yield return "A type cast must be close to an argument. Sample: <myArg>[string] or [string]<myArg>";
                    }
                }
            }
        }
    }
}