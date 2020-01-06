using System;
using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    internal class PrimitiveTypeNameValidator : IPrimitiveTypeNameValidator
    {
        private readonly IEnumerable<string> _strings;

        public PrimitiveTypeNameValidator()
        {
            var invalidNames = new[] { "bool", "int", "long" };
            _strings = typeof(Convert).GetMethods().Where(m => m.Name.StartsWith("To")).Select(m => m.Name.Replace("To", string.Empty)).Distinct().Concat(invalidNames).ToList();
        }

        public ValidationResult IsValid(string value)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var match = _strings.Where(s => s.ToLower().EqualsTo(value.ToLower()));
            return new ValidationResult(match.IsEmpty(), match.Flatten(Environment.NewLine));
        }
    }
}
