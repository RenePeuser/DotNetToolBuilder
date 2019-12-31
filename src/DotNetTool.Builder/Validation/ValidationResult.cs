namespace DotNetTool.Builder.Validation
{
    using System.Diagnostics;

    [DebuggerDisplay("IsValid: {IsValid}")]
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