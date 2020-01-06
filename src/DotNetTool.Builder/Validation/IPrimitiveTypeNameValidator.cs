namespace DotNetTool.Builder.Validation
{
    internal interface IPrimitiveTypeNameValidator
    {
        ValidationResult IsValid(string value);
    }
}
