using System.Diagnostics;

namespace DotNetTool.Builder.Validation
{
    [DebuggerDisplay("Validate: {" + nameof(IsValid) + "}")]
    internal class ValidationResult
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
