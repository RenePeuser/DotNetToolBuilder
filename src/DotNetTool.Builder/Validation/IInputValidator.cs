namespace DotNetTool.Builder.Validation
{
    public interface IInputValidator
    {
        ValidationResult IsValid(string value);
    }

    public class ValidationResult
    {
        public ValidationResult(bool isValid, string errors)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public bool IsValid { get; }
        public string Errors { get; }
    }
}