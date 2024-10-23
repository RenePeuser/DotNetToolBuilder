using System;
using System.Diagnostics;

namespace DotNetTool.Builder.Models.Validation
{
    [DebuggerDisplay("Validate: '{" + nameof(IsValid) + "}")]
    internal sealed class PrimitiveTypeValidationResult(string errors,
                                                        Type type,
                                                        string alias) : ValidationResult(errors)
    {
        public Type Type { get; } = type;

        public string Alias { get; } = alias;
    }
}