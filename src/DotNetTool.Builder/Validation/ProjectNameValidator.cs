using System;
using System.Collections.Generic;

namespace DotNetTool.Builder.Validation
{
    public class ProjectNameValidator : ValidatorBase, IProjectNameValidator
    {
        public override IEnumerable<Predicate<char>> GetValidationRules()
        {
            yield return char.IsLetterOrDigit;
            yield return c => c == '.';
        }

        public override string GetValidationInfo()
        {
            return "Only letters, digits or '.' are allowed";
        }
    }
}