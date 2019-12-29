namespace DotNetTool.Builder.Validation
{
    public interface IPrimitiveTypeNameValidator
    {
        ValidationResult IsValid(string value);
    }
}