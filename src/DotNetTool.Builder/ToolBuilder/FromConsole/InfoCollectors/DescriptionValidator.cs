using System;
using System.Collections.Generic;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class DescriptionValidator : IDescriptionValidator
    {
        public ValidationResult Validate(string value)
        {
            var errors = CollectErrors(value).Flatten(Environment.NewLine);
            return new ValidationResult(errors);
        }

        private IEnumerable<string> CollectErrors(string value)
        {
            var trimmedValue = value.Trim();
            if (trimmedValue.IsNullOrEmpty())
            {
                yield return "Description must not be null, empty or whitespace.";
            }

            if (trimmedValue.Length < 10)
            {
                yield return $"Description: '{value}' should have minimum 10 characters for a good description";
            }
        }
    }
}
