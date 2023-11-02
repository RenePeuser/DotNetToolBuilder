using System;
using System.IO;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class PrimitiveTypeNameValidator : IPrimitiveTypeNameValidator
    {
        private readonly IBuiltInTypeTableService _builtInTypeTableService;
        private readonly Type[] _supportedTypes;

        public PrimitiveTypeNameValidator(IBuiltInTypeTableService builtInTypeTableService)
        {
            _builtInTypeTableService = builtInTypeTableService;
            var systemTypes = typeof(double).Assembly.GetTypes();
            var systemIoTypes = typeof(FileInfo).Assembly.GetTypes();

            _supportedTypes = systemTypes.Concat(systemIoTypes).Where(t => (t.IsAbstract && t.IsSealed).IsFalse() && t.FullName.StartsWith("System.") && t.Name.All(char.IsLetterOrDigit)).ToArray();
        }

        public PrimitiveTypeValidationResult IsTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(value);

            var primitiveType = _builtInTypeTableService.GetTypeFor(value);
            if (primitiveType.IsNotNull())
            {
                return new PrimitiveTypeValidationResult(string.Empty, primitiveType.Type, primitiveType.Alias);
            }

            var lowerTypeName = value.ToLower();
            var typeMatch = _supportedTypes.Where(t => t.Name.ToLower().EqualsTo(lowerTypeName) || t.FullName.ToLower().EqualsTo(lowerTypeName)).ToList().FirstOrDefault();

            var errorMessage = $"The type: '{value}' for the argument type cast: '[{value}]' is not a valid system type. Sample: <myArg>[string] or <myArg>[FileInfo] or many more.";
            return new PrimitiveTypeValidationResult(typeMatch.IsNull() ? errorMessage : string.Empty, typeMatch, typeMatch?.Name);
        }

        public PrimitiveTypeValidationResult IsPrimitiveTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(value);

            var primitiveType = _builtInTypeTableService.GetTypeFor(value);
            if (primitiveType.IsNotNull())
            {
                return new PrimitiveTypeValidationResult(string.Empty, primitiveType.Type, primitiveType.Alias);
            }

            var errorMessage = $"The type: '{value}' is not a primitive system type";
            return new PrimitiveTypeValidationResult(errorMessage, null, null);
        }
    }
}
