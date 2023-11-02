using System.Linq;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class OptionAliasValidator : IOptionAliasValidator
    {
        public ValidationResult Validate(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return new ValidationResult("Alias for an option must not be null, empty or whitespace");
            }

            if (value.TrimStart('-').All(char.IsLetterOrDigit).IsFalse())
            {
                return new ValidationResult("Alias for an option must contains only letters, numbers and starts with a '-'");
            }

            if (char.IsLetter(value.First()))
            {
                return new ValidationResult("Alias for an option must begin with a '-'");
            }

            var secondLeter = value.ElementAt(1);
            if (secondLeter.IsNull())
            {
                return new ValidationResult("Alias for an option must begin with '-' and must have one letter. Sample: '-o'");
            }

            if (char.IsLetter(secondLeter).IsFalse())
            {
                return new ValidationResult("Alias for an option must have minimum one letter after '-'. Sample: '-o'");
            }

            return new ValidationResult(string.Empty);
        }
    }
}
