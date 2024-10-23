using System.Collections.Generic;
using DotNetTool.Builder.Services.Optimizer;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal sealed class ValidateTypeFromString(IPrimitiveTypeNameValidator primitiveTypeNameValidator,
                                                 IArgumentTypeOptimizer argumentTypeOptimizer)
    {
        internal IEnumerable<string> CollectErrors(string typeName)
        {
            if (typeName.IsNullOrWhiteSpace())
            {
                yield break;
            }

            var optimizedTypeName = argumentTypeOptimizer.OptimizeType(typeName);
            var isValidTypeNameResult = primitiveTypeNameValidator.IsTypeName(optimizedTypeName);
            if (isValidTypeNameResult.IsValid.IsFalse())
            {
                yield return isValidTypeNameResult.Errors;
            }
        }
    }
}