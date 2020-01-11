using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Validation;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal class SystemTypeNameOptimizer : ITypeNameOptimizer
    {
        // This is because we do not want use the real Systemtypes if it is possible.
        // Sample for 'object' find result will be 'Object' but we want prefer the low letter
        // case 'object' in such cases.
        private static readonly string[] typeExceptions = new[]
        {
            "object",
            "string",
            "byte",
            "sbyte",
            "double",
            "decimal",
            "char",
            "bool",
            "int",
            "long",
        };

        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public SystemTypeNameOptimizer(IPrimitiveTypeNameValidator primitiveTypeNameValidator)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
        }

        public string Optimize(string typeName)
        {
            // first check 99% case the primitive types
            var lowletterType = typeExceptions.FirstOrDefault(te => te.EqualsTo(typeName.ToLower()));
            if (lowletterType.IsNotNull())
            {
                return lowletterType;
            }

            // if we have a special type look ind System and System.IO namespace
            var result = _primitiveTypeNameValidator.IsTypeName(typeName);
            if (result.IsValid)
            {
                return result.Type.Name;
            }

            return typeName;
        }

        public bool OptimizerFor(string typeName)
        {
            return true;
        }
    }
}