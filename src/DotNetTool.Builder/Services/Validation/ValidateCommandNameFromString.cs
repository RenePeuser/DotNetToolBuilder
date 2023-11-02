using System.Collections.Generic;
using System.Linq;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateCommandNameFromString
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ValidateCommandNameFromString(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        internal IEnumerable<string> CollectErrors(string command)
        {
            if (command.Contains("--") || command.Contains("<") || command.Contains("["))
            {
                yield return $"The command: '{command}' most not contain argument '<arg>', typecast '[type]' or option-syntax '[--option]'";
            }

            if (char.IsLetter(command.First()).IsFalse())
            {
                yield return $"The command: '{command}' must begin with a letter";
            }

            if (command.All(char.IsLetterOrDigit).IsFalse())
            {
                yield return $"The command: '{command}' must only contains letters or digits";
            }

            var validationResult = _primitiveTypeNameValidator.IsTypeName(command);
            if (validationResult.IsValid)
            {
                yield return $"The command: '{command}' must not be a name of a type";
            }
        }
    }
}
