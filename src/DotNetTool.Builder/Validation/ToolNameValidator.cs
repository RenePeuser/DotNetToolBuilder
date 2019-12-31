using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    using Argument.Check;

    public class ToolNameValidator : IToolNameValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ToolNameValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            Throw.IfNull(() => primitiveTypeNameValidator);

            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string value)
        {
            // No argument check here !
            // Throw.IfNullOrWhiteSpace(() => value);

            var errors = CollectErrors(value).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsEmpty(), errors);
        }

        private IEnumerable<string> CollectErrors(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                yield return "The dotnet tool name must not be null, empty or whitespace";
                yield break;
            }

            if (value.Contains(" "))
            {
                yield return $"The dotnet tool name: '{value}' must not contains whitespace";
                yield break;
            }

            if (value.All(char.IsLetterOrDigit).IsFalse())
            {
                yield return $"The dotnet tool: {value} name must only contains letters or digits";
            }

            if (char.IsLetter(value.First()).IsFalse())
            {
                yield return $"The dotnet tool name: '{value}' must start with a letter";
            }

            var validationResult = _primitiveTypeNameValidator.IsValid(value);
            if (validationResult.IsValid.IsFalse())
            {
                yield return $"The dotnet tool name: '{value}' must not be a name of a type";
            }
        }
    }
}