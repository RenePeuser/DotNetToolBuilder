using System.Diagnostics;
using Extensions.Pack;

namespace DotNetTool.Builder.Models.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal class ValidationResult
    {
        internal ValidationResult(string errors)
        {
            Errors = errors;
            IsValid = errors.IsNullOrEmpty();
        }

        internal bool IsValid { get; }

        internal string Errors { get; }
    }
}
