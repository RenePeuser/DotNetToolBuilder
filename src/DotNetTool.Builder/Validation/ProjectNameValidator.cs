using DotNetTool.Builder.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Validation
{
    public class ProjectNameValidator : IProjectNameValidator
    {
        private readonly IEnumerable<Predicate<char>> _validationRules = new Predicate<char>[]
        {
            char.IsLetterOrDigit,
            c => c == '.'
        };

        public ValidationResult IsValid(string value)
        {
            var errors = CollectErrors(value).Flatten(Environment.NewLine);
            return new ValidationResult(errors.IsEmpty(), errors);
        }

        private IEnumerable<string> CollectErrors(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                yield return "The project name must not be null, empty or whitespace";
                yield break;
            }

            if (value.All(c => _validationRules.Any(validation => validation(c))).IsFalse())
            {
                yield return "The project name must only contains letters, digits or '.' are allowed.";
            }

            if (char.IsLetter(value.First()).IsFalse())
            {
                yield return "The project name must start with a letter";
            }

            if (char.IsLetter(value.Last()).IsFalse())
            {
                yield return "The project name must end with a letter";
            }
        }
    }
}