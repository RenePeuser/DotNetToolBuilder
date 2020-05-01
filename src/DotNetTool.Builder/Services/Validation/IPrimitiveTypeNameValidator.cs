using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.Services.Validation
{
    internal interface IPrimitiveTypeNameValidator
    {
        PrimitiveTypeValidationResult IsTypeName(string value);
        PrimitiveTypeValidationResult IsPrimitiveTypeName(string value);
    }
}
