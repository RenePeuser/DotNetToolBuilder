namespace DotNetTool.Builder.Validation
{
    public interface IInputValidator
    {
        ValidationResult IsValid(string value);
    }
}