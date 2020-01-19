
using DotNetTool.Builder.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal class SystemTypeNameOptimizer : ITypeNameOptimizer
    {
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;
        private readonly IBuiltInTypeTableService _builtInTypeTableService;

        public SystemTypeNameOptimizer(IPrimitiveTypeNameValidator primitiveTypeNameValidator, IBuiltInTypeTableService builtInTypeTableService)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
            _builtInTypeTableService = builtInTypeTableService;
        }

        public string Optimize(string typeName)
        {
            // first check 99% case the primitive types
            var lowletterType = _builtInTypeTableService.GetTypeFor(typeName);
            if (lowletterType.IsNotNull())
            {
                return lowletterType.Alias;
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