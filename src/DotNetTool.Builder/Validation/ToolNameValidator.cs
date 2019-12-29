using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    public class ToolNameValidator : IToolNameValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ToolNameValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult IsValid(string value)
        {
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

            if (value.All(char.IsLetterOrDigit).IsFalse())
            {
                yield return "The dotnet tool name must only contains letters, digits or '.' are allowed.";
            }

            var validationResult = _primitiveTypeNameValidator.IsValid(value);
            if (validationResult.IsValid.IsFalse())
            {
                yield return $"The dotnet tool name: '{value}' must not be a name of a type";
            }
        }
    }
}