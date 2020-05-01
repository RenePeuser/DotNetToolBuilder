using System.Diagnostics;
using Extensions.Pack;

namespace DotNetTool.Builder.Models.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal class ValidationResult
    {
        public ValidationResult(string errors)
        {
            Errors = errors;
            IsValid = errors.IsNullOrEmpty();
        }

        public bool IsValid { get; }

        public string Errors { get; }
    }
}
