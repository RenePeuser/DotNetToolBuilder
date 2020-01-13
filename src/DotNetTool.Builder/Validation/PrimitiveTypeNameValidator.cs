using System;
using System.IO;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.Validation
{
   
    internal class PrimitiveTypeNameValidator : IPrimitiveTypeNameValidator
    {
        private readonly IBuiltInTypeTableService _builtInTypeTableService;
        private readonly Type[] supportedTypes;

        public PrimitiveTypeNameValidator(IBuiltInTypeTableService builtInTypeTableService)
        {
            _builtInTypeTableService = builtInTypeTableService;
            var systemTypes = typeof(double).Assembly.GetTypes();
            var systemIoTypes = typeof(FileInfo).Assembly.GetTypes();

            supportedTypes = systemTypes.Concat(systemIoTypes).Where(t => (t.IsAbstract && t.IsSealed).IsFalse() && t.FullName.StartsWith("System.") && t.Name.All(char.IsLetterOrDigit)).ToArray();
        }

        public PrimitiveTypeValidationResult IsTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var primitiveType = _builtInTypeTableService.GetTypeFor(value);
            if (primitiveType.IsNotNull())
            {
                return new PrimitiveTypeValidationResult(true, "", primitiveType.Type, primitiveType.Alias);
            }

            var lowerTypeName = value.ToLower();
            var typeMatch = supportedTypes.Where(t => t.Name.ToLower().EqualsTo(lowerTypeName) || t.FullName.ToLower().EqualsTo(lowerTypeName)).ToList().FirstOrDefault();

            var errorMessage = $"The type: '{value}' for the argument type cast: '[{value}]' is not a valid system type. Sample: <myArg>[string] or <myArg>[FileInfo] or many more.";
            return new PrimitiveTypeValidationResult(typeMatch.IsNotNull(), errorMessage, typeMatch, typeMatch?.Name);
        }

        public ValidationResult IsNotTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var match = supportedTypes.Where(t => t.Name.ToLower().EqualsTo(value.ToLower())).Select(t => t.Name).ToList();
            return new ValidationResult(match.IsEmpty(), match.Flatten(Environment.NewLine));
        }
    }
}
