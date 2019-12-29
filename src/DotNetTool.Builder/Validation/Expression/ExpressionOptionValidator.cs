using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;

    public class ExpressionOptionValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ExpressionOptionValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
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

            var splittedExpression = expression.Split(" ");
            foreach (var value in splittedExpression)
            {
                if (value.StartsWith("-"))
                {
                    if (value.StartsWith("--").IsFalse())
                    {
                        yield return $"Option: '{value}' must start with '--'";
                    }
                    else
                    {
                        var optionName = value.Replace("--", string.Empty);
                        var validationResult = _primitiveTypeNameValidator.IsValid(optionName);
                        if (validationResult.IsValid.IsFalse())
                        {
                            yield return $"The name of an argument does not match a name of a type: {optionName}";
                        }
                    }
                }
            }
        }
    }
}