namespace DotNetTool.Builder.Validation
{
    internal interface IInputValidator
    {
        ValidationResult IsValid(string value);
    }
}