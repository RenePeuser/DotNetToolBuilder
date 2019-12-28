using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    public class ToolNameValidator : IToolNameValidator
    {
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
        }
    }
}