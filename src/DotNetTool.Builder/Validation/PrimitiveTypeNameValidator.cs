using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    internal class PrimitiveTypeNameValidator : IPrimitiveTypeNameValidator
    {
        // ToDo: centralize this later.
        // This is because we do not want use the real Systemtypes if it is possible.
        // Sample for 'object' find result will be 'Object' but we want prefer the low letter
        // case 'object' in such cases.
        private static readonly Dictionary<string, Type> typeExceptions = new Dictionary<string, Type>
        {
            { "object", typeof(object) },
            { "string", typeof(string) },
            { "byte", typeof(byte) },
            { "sbyte", typeof(sbyte) },
            { "double", typeof(double) },
            { "decimal", typeof(decimal) },
            { "char", typeof(char) },
            { "bool", typeof(bool) },
            { "int", typeof(int) },
            { "long", typeof(long) }
        };

        private readonly Type[] supportedTypes;

        public PrimitiveTypeNameValidator()
        {
            var systemTypes = typeof(double).Assembly.GetTypes();
            var systemIoTypes = typeof(FileInfo).Assembly.GetTypes();

            supportedTypes = systemTypes.Concat(systemIoTypes).Where(t => (t.IsAbstract && t.IsSealed).IsFalse() && t.FullName.StartsWith("System.") && t.Name.All(char.IsLetterOrDigit)).ToArray();
        }

        public PrimitiveTypeValidationResult IsTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var primitiveType = typeExceptions.GetValueOrDefault(value, null);
            if (primitiveType.IsNotNull())
            {
                return new PrimitiveTypeValidationResult(true, "", primitiveType);
            }

            var lowerTypeName = value.ToLower();
            var match = supportedTypes.Where(t => t.Name.ToLower().EqualsTo(lowerTypeName) || t.FullName.ToLower().EqualsTo(lowerTypeName)).ToList();

            var errorMessage = $"The type: '{value}' for the argument type cast: '[{value}]' is not a valid system type. Sample: <myArg>[string] or <myArg>[FileInfo] or many more.";
            return new PrimitiveTypeValidationResult(match.Any(), errorMessage, match.FirstOrDefault());
        }

        public ValidationResult IsNotTypeName(string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var match = supportedTypes.Where(t => t.Name.ToLower().EqualsTo(value.ToLower())).Select(t => t.Name).ToList();
            return new ValidationResult(match.IsEmpty(), match.Flatten(Environment.NewLine));
        }
    }
}
