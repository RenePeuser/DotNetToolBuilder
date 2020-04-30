using System;
using System.Diagnostics;

namespace DotNetTool.Builder.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal class PrimitiveTypeValidationResult : ValidationResult
    {
        public PrimitiveTypeValidationResult(string errors, Type type, string alias) : base(errors)
        {
            Type = type;
            Alias = alias;
        }

        public Type Type { get; }

        public string Alias { get; }
    }
}