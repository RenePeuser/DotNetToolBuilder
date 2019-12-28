using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Validation.Expression
{
    public class ExpressionToolNameValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            return new ValidationResult(IsValidInternal(dotNetToolName, expression), $"Expression must start with your defined dotnet tool name: '{dotNetToolName}'");
        }

        private bool IsValidInternal(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                return false;
            }

            return expression.StartsWith(dotNetToolName);
        }
    }
}