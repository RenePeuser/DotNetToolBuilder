namespace DotNetTool.Builder.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;

    public class PrimitiveTypeNameValidator : IPrimitiveTypeNameValidator
    {
        private readonly IEnumerable<string> _strings;

        public PrimitiveTypeNameValidator()
        {
            var invalidNames = new string[] { "bool", "int" };
            _strings = typeof(Convert).GetMethods().Where(m => m.Name.StartsWith("To")).Select(m => m.Name.Replace("To", string.Empty)).Distinct().Concat(invalidNames).ToList();
        }

        public ValidationResult IsValid(string value)
        {
            var match = _strings.Where(s => s.ToLower().EqualsTo(value.ToLower()));
            return new ValidationResult(match.IsEmpty(), match.Flatten(Environment.NewLine));
        }
    }
}