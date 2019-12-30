using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    using System;
    using System.Collections.Generic;

    public class ArgumentValidator : IExpressionContentValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ArgumentValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            var errors = CheckForErrors(expression).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsEmpty(), errors);
        }


        // Valid argument declarations
        // <arg>
        // <arg>[string]
        // [string]<arg>
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
                if (value.IsNullOrWhiteSpace())
                {
                    continue;
                }

                if (value.DoesNotContain("<") && value.DoesNotContain(">"))
                {
                    continue;
                }

                if(value.Count(c => c == '<' || c == '>').NotEqualsTo(2))
                {
                    yield return $"Argument: '{value}' contains another argument syntax. Multiple '<' or '>' are not valid";
                    continue;
                }

                if (value.IndexOf('<') > value.IndexOf('>'))
                {
                    yield return $"Argument: '{value}' must starts with '< and ends with '>'";
                    continue;
                }

                var start = value.IndexOf("<") + 1;
                var end = value.IndexOf(">");
                var argumentName = value.Substring(start, end - start);
                if (argumentName.IsNullOrWhiteSpace())
                {
                    yield return $"Missing argument name: {value}";
                    yield break;
                }

                var validationResult = _primitiveTypeNameValidator.IsValid(argumentName);
                if (validationResult.IsValid.IsFalse())
                {
                    yield return $"The name of an argument does not match a name of a type: {argumentName}";
                }

                if (value.Contains("--"))
                {
                    yield return $"Argument '{value}' contains '--' is only allowed for options, to separate verbs use '-'";
                }

                var preCast = value.Split('<').First();
                if (preCast.IsNotNullOrEmpty())
                {
                    if (preCast.StartsWith("[").IsFalse() || preCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Argument: {value} is invalid, only a type cast can be attached to an argument. Sample: [string]<arg>";
                    }
                }

                var postCast = value.Split('>').Last();
                if (postCast.IsNotNullOrEmpty())
                {
                    if (postCast.StartsWith("[").IsFalse() || postCast.EndsWith("]").IsFalse())
                    {
                        yield return $"Argument: {value} is invalid, only a type cast can be attached to an argument. Sample: <arg>[string]";
                    }
                }
            }
        }
    }
}