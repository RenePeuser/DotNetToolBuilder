using System.Collections.Generic;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.Validation
{
    internal class ValidateTypeFromString
    {
        private readonly IArgumentTypeOptimizer _argumentTypeOptimizer;
        private readonly IPrimitiveTypeNameValidator _primitiveTypeNameValidator;

        public ValidateTypeFromString(IPrimitiveTypeNameValidator primitiveTypeNameValidator, IArgumentTypeOptimizer argumentTypeOptimizer)
        {
            _primitiveTypeNameValidator = primitiveTypeNameValidator;
            _argumentTypeOptimizer = argumentTypeOptimizer;
        }

        internal IEnumerable<string> CollectErrors(string typeName)
        {
            if (typeName.IsNullOrWhiteSpace())
            {
                yield break;
            }

            var optimizedTypeName = _argumentTypeOptimizer.OptimizeType(typeName);
            var isValidTypeNameResult = _primitiveTypeNameValidator.IsTypeName(optimizedTypeName);
            if (isValidTypeNameResult.IsValid.IsFalse())
            {
                yield return isValidTypeNameResult.Errors;
            }
        }
    }
}