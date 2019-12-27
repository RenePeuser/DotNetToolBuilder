using System;
using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation
{
    public abstract class ValidatorBase : IInputValidator
    {
        public bool IsValid(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            return value.All(c => GetValidationRules().Any(validation => validation(c)));
        }

        public abstract string GetValidationInfo();

        public abstract IEnumerable<Predicate<char>> GetValidationRules();
    }
}