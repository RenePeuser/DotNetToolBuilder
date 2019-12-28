using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class ExpressionCastValidator : IExpressionContentValidator
    {
        private const string ValidationInfo = "Cast expressions must be open with '[' and closed with ']'";

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            return new ValidationResult(IsValidInternal(expression), ValidationInfo);
        }

        private bool IsValidInternal(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            var splittedExpression = value.Split(" ");
            var result = splittedExpression.Where(s =>
            {
                if (s.Contains("[") || s.Contains("]"))
                {
                    return s.Count(c => c == '[' || c == ']') != 2 || s.IndexOf('[') > s.IndexOf(']');
                }

                return false;
            });
            return result.IsEmpty();

        }
    }
}