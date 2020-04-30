using System.Diagnostics;
using System.Linq;
using Extensions.Pack;

namespace DotNetTool.Builder.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal class ValidationResult
    {
        public ValidationResult(string errors)
        {
            Errors = errors;
            IsValid = errors.IsNotNullOrEmpty();
        }

        public bool IsValid { get; }

        public string Errors { get; }
    }
}
