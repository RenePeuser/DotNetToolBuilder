using System;
using System.Diagnostics;
using Argument.Check;

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

    [DebuggerDisplay("Validate: {" + nameof(IsValid) + "}")]
    internal class PrimitiveTypeValidationResult : ValidationResult
    {
        public PrimitiveTypeValidationResult(bool isValid, string errors, Type type) : base(isValid, errors)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
