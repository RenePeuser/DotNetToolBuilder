using System;
using System.Diagnostics;

namespace DotNetTool.Builder.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal class PrimitiveTypeValidationResult : ValidationResult
    {
        public PrimitiveTypeValidationResult(bool isValid, string errors, Type type, string alias) : base(isValid, errors)
        {
            Type = type;
            Alias = alias;
        }

        public Type Type { get; }

        public string Alias { get; }
    }
}