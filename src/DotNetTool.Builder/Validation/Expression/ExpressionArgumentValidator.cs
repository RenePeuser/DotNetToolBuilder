using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;

    public class ExpressionArgumentValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ExpressionArgumentValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errors = CheckForErrors(expression).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsEmpty(), errors);
        }

        private IEnumerable<string> CheckForErrors(string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                yield return "Argument must not be null or empty.";
                yield break;

            }

            var splittedExpression = expression.Split();
            foreach (var value in splittedExpression)
            {
                if (value.Contains("<") || value.Contains(">"))
                {
                    var result = value.Count(c => c == '<' || c == '>') != 2 || value.IndexOf('<') > value.IndexOf('>');
                    if (result)
                    {
                        yield return $"Argument: '{value}' must starts with '< and ends with '>'";
                    }
                    else
                    {
                        var start = value.IndexOf("<") + 1;
                        var end = value.IndexOf(">");
                        var argumentName = value.Substring(start, end - start);
                        var validationResult = _primitiveTypeNameValidator.IsValid(argumentName);
                        if (validationResult.IsValid.IsFalse())
                        {
                            yield return $"The name of an argument does not match a name of a type: {argumentName}";
                        }

                        if (value.Contains("--"))
                        {
                            yield return $"Argument '{value}' contains '--' is only allowed for options, to separate verbs use '-'";
                        }
                    }
                }
            }
        }
    }
}