namespace DotNetTool.Builder.Validation.Expression
{
    using Extensions;

    public class ExpressionMinimumCommandValidator : IExpressionContentValidator
    {
        public ValidationResult IsValid(string dotNetToolName, string expression)
        {
            return new ValidationResult(IsValidInternal(dotNetToolName, expression), $"Your dot net tool expression: '{expression}' must have minimum one command.");
        }

        private bool IsValidInternal(string dotNetToolName, string expression)
        {
            if (expression.IsNullOrWhiteSpace())
            {
                return false;
            }

            var split = expression.Split(' ');
            return split.Length > 1;
        }
    }
}