using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
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

        public ValidationResult Validate(string value)
        {
            // No argument check here !
            // Throw.IfNullOrWhiteSpace(() => value);

            var errors = CollectErrors(value).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
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

            // A dot is allowed in project name, so we have to check that not any part which is separated
            // by a dot is a type name, otherwise we can not compile because of namespace conflicts.
            var splittedProjectName = value.Split('.');

            // ToDo: add new special naming validation, beacause of causing namespace conflicts.
            if (splittedProjectName.Any(s => s.EqualsTo("new")))
            {
                yield return $"The project name: '{value}' must not contains '.new.' because of coming namespace conflicts.";
            }

            var validationResults = splittedProjectName.Select(name => _primitiveTypeNameValidator.IsTypeName(name));
            foreach (var validationResult in validationResults)
            {
                if (validationResult.IsValid)
                {
                    yield return $"The project, or a part of the project name: '{validationResult.Alias}' must not contains a name of a system type";
                }
            }
        }
    }
}
