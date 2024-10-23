using DotNetTool.Builder.Services.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal sealed class SystemTypeNameOptimizer(IPrimitiveTypeNameValidator primitiveTypeNameValidator,
                                                  IBuiltInTypeTableService builtInTypeTableService) : ITypeNameOptimizer
    {
        public string Optimize(string typeName)
        {
            // first check 99% case the primitive types
            var lowletterType = builtInTypeTableService.GetTypeFor(typeName);
            if (lowletterType.IsNotNull())
            {
                return lowletterType.Alias;
            }

            // if we have a special type look ind System and System.IO namespace
            var result = primitiveTypeNameValidator.IsTypeName(typeName);
            if (result.IsValid)
            {
                return result.Type.Name;
            }

            return typeName;
        }
    }
}