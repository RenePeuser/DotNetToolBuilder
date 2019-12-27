using System;
using System.Collections.Generic;

namespace DotNetTool.Builder.Validation
{
    public class ToolNameValidator : ValidatorBase, IToolNameValidator
    {
        public override IEnumerable<Predicate<char>> GetValidationRules()
        {
            yield return char.IsLetterOrDigit;
        }

        public override string GetValidationInfo()
        {
            return "Only letters or digits are allowed";
        }
    }
}