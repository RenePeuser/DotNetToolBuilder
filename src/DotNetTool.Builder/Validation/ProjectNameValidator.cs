using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    internal class ProjectNameValidator : IProjectNameValidator
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        private readonly IEnumerable<Predicate<char>> _validationRules = new Predicate<char>[] { char.IsLetterOrDigit, c => c == '.' };

        public ProjectNameValidator(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
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
                yield return $"The project name: '{value}' must not be null, empty or whitespace";
                yield break;
            }

            if (value.Contains(" "))
            {
                yield return $"The project name: '{value}' must not contains whitespace";
                yield break;
            }

            if (value.All(c => _validationRules.Any(validation => validation(c))).IsFalse())
            {
                yield return $"The project name: '{value}' must only contains letters, digits or '.' are allowed";
            }

            if (char.IsLetter(value.First()).IsFalse())
            {
                yield return $"The project name: '{value}' must start with a letter";
            }

            if (char.IsLetterOrDigit(value.Last()).IsFalse())
            {
                yield return $"The project name: '{value}' must end with a letter or digits";
            }

            var validationResult = _primitiveTypeNameValidator.IsValid(value);
            if (validationResult.IsValid.IsFalse())
            {
                yield return $"The project name: '{value}' must not be a name of a type";
            }
        }
    }
}
