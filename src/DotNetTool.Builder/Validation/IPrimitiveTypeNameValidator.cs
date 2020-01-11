using System.Collections.Generic;

namespace DotNetTool.Builder.Validation
{
    internal interface IPrimitiveTypeNameValidator
    {
        PrimitiveTypeValidationResult IsTypeName(string value);
    }
}
