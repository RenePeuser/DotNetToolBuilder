using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class ToolNameValidator : IToolNameValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ToolNameValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            Throw.IfNull(() => primitiveTypeNameValidator);

            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public ValidationResult Validate(string value, string projectName)
        {
            // No argument check here !
            // Throw.IfNullOrWhiteSpace(() => value);

            var errors = CollectErrors(value, projectName).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(string value, string projectName)
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

            if (value.All(c => char.IsLetterOrDigit(c) || c == '-').IsFalse())
            {
                yield return $"The dotnet tool: '{value}' name must only contains letters, digits or '-'.";
            }

            if (char.IsLetter(value.First()).IsFalse())
            {
                yield return $"The dotnet tool name: '{value}' must start with a letter";
            }

            var splitProjectName = projectName.Split(".");
            if (splitProjectName.First().EqualsTo(value.FirstCharToUpper()))
            {
                yield return $"The dotnet tool name: '{value}' must not equal with the start of your project name: '{projectName}', causes in namespace conflicts. Hint the dotnet tool name will transformed to: {value.FirstCharToUpper()}";
            }


            var validationResult = _primitiveTypeNameValidator.IsTypeName(value);
            if (validationResult.IsValid)
            {
                yield return $"The dotnet tool name: '{value}' must not be a name of a type";
            }
        }
    }
}
