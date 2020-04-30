using System.Collections.Generic;
using System.Linq;
using Extensions.Pack;

namespace DotNetTool.Builder.Validation
{
    internal class ValidateArgumentNameFromString
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ValidateArgumentNameFromString(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        internal IEnumerable<string> CheckForErrors(string argumentName)
        {
            if (argumentName.IsNullOrWhiteSpace())
            {
                yield return $"Missing argument name: '{argumentName}'";
                yield break;
            }

            if (char.IsLetter(argumentName.First()).IsFalse())
            {
                yield return $"The argument: '{argumentName}' must begin with a letter";
            }

            var validationResult = _primitiveTypeNameValidator.IsTypeName(argumentName);
            if (validationResult.IsValid)
            {
                yield return $"The argument: '{argumentName}' must not be a name of a type.";
            }
        }
    }
}