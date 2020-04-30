using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Validation;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Validation
{
    internal class DotNetToolValidationResult
    {
        public DotNetToolValidationResult(IEnumerable<ValidationResult> result)
        {
            ValidationResults = result;
            HasErrors = result.Any(r => r.IsValid.IsNot());
        }

        internal IEnumerable<ValidationResult> ValidationResults { get; }

        internal bool HasErrors { get; }
    }
}