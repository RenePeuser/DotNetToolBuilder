namespace DotNetTool.Builder.Validation.Expression
{
    using Models;

    public interface IExpressionContentValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}