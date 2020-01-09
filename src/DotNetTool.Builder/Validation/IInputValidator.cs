namespace DotNetTool.Builder.Validation
{
    internal interface IInputValidator
    {
        ValidationResult Validate(string value);
    }
}
