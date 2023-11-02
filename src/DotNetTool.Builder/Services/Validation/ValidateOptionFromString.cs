using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateOptionFromString
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ValidateOptionFromString(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            Throw.IfNull(() => primitiveTypeNameValidator);

            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        internal IEnumerable<string> CollectErrors(string option)
        {
            if (option.IsNullOrEmpty())
            {
                yield break;
            }

            var test = option.Split("--");
            if (test[1].StartsWith("-"))
            {
                yield return $"The option: '{option}' must start with: '--'. Sample: '--option' or --my-option";
                yield break;
            }

            var optionName = option.TrimStart('-');
            if (optionName.IsNullOrWhiteSpace())
            {
                yield return $"The option: '{option}' is missing name";
                yield break;
            }

            if (optionName.Contains("<") || optionName.Contains(">"))
            {
                yield return $"The option: '{option}' contains argument syntax, please separate the argument with a whitespace";
            }

            if (optionName.Contains("[") || optionName.Contains("]"))
            {
                yield return $"The option: '{option}' contains type cast syntax, type cast is only valid at argument";
            }

            if (optionName.Contains("--"))
            {
                yield return $"The option: '{option}' contains '--' is only allowed at the beginning, to separate verbs use '-'";
            }

            if (char.IsLetterOrDigit(option.Last()).IsFalse())
            {
                yield return $"The option: '{option}' must ends only with a letter or digit";
            }

            if (char.IsLetter(optionName.First()).IsFalse())
            {
                yield return $"The option: '{optionName}' must begin with a letter";
            }

            if (optionName.Contains("."))
            {
                yield return $"The option: '{optionName}' must not contains '.'";
            }

            var validationResult = _primitiveTypeNameValidator.IsPrimitiveTypeName(optionName);
            if (validationResult.IsValid)
            {
                yield return $"The name of an option does not match a name of a type: '{optionName}'";
            }
        }
    }
}