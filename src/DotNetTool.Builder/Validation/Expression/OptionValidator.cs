using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OptionValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public OptionValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
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
                if (value.StartsWith("-"))
                {
                    if (value.StartsWith("--").IsFalse())
                    {
                        yield return $"Option: '{value}' must start with '--'";
                        continue;
                    }

                    var optionName = value.TrimStart('-');

                    if (optionName.IsNullOrWhiteSpace())
                    {
                        yield return $"Declaration: '{value}' needs an option name, check your expression: {expression}";
                        continue;
                    }

                    if (optionName.Contains("<") || optionName.Contains(">"))
                    {
                        yield return $"Option '{value}' contains argument syntax, please separate the argument with a whitespace";
                    }

                    if (optionName.Contains("[") || optionName.Contains("]"))
                    {
                        yield return $"Option '{value}' contains type cast syntax, type cast is only valid at argument";
                    }

                    if (optionName.Contains("--"))
                    {
                        yield return $"Option '{value}' contains '--' is only allowed at the beginning, to separate verbs use '-'";
                    }

                    if (char.IsLetterOrDigit(value.Last()).IsFalse())
                    {
                        yield return $"Option: '{value}' must ends only with a letter or digit";
                    }

                    if (char.IsLetter(optionName.First()).IsFalse())
                    {
                        yield return $"Option: {optionName} must begin with a letter.";
                    }

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