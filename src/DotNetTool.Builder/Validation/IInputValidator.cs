namespace DotNetTool.Builder.Validation
{
    public interface IInputValidator
    {
        bool IsValid(string value);
        string GetValidationInfo();
    }
}