using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class ExpressionCastValidator : IExpressionContentValidator
    {
        private const string ValidationInfo = "Cast expressions must be open with '[' and closed with ']'";

        public ValidationResult IsValid(string value)
        {
            return new ValidationResult(IsValidInternal(value), ValidationInfo);
        }

        private bool IsValidInternal(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            var count = value.Count(c => c == '[') + value.Count(c => c == ']');
            return count % 2 == 0;
        }
    }
}