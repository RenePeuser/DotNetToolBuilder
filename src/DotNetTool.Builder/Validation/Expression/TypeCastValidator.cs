using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;

    public class TypeCastValidator : IExpressionContentValidator
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
                if (value.Contains("[") || value.Contains("]"))
                {
                    if (value.Count(c => c == '[' || c == ']') != 2 || value.IndexOf('[') > value.IndexOf(']'))
                    {
                        yield return "A type cast must begin with '[' and ends with ']'";
                        continue;
                    }

                    if (value.StartsWith("[") && value.EndsWith("]"))
                    {
                        yield return "A type cast must be close to an argument. Sample: <myArg>[string] or [string]<myArg>";
                        continue;
                    }

                    var start = value.IndexOf("[") + 1;
                    var end = value.IndexOf("]");
                    var typeName = value.Substring(start, end - start);

                    if (typeName.All(c => char.IsLetterOrDigit(c) || c == '.').IsFalse())
                    {
                        yield return $"Type: '{typeName}' must only contains letter, digits or '.'";
                        continue;
                    }
                    if (char.IsLetter(typeName.First()).IsFalse())
                    {
                        yield return $"Type: '{typeName}' must begin with a letter";
                    }
                }
            }
        }
    }
}