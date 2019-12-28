using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class ExpressionOptionValidator : IExpressionContentValidator
    {
        private const string ValidationInfo = "Options must be declared with '--'";

        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            return new ValidationResult(IsValidInternal(expression), ValidationInfo);
        }

        private static bool IsValidInternal(string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return false;
            }

            var split = value.Split(" ");
            return split.Where(s => s.StartsWith("-")).All(s => s.StartsWith("--"));
        }
    }
}