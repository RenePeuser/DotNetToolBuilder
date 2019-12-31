namespace DotNetTool.Builder.Validation.Expression
{
    using Models;

    public interface IExpressionValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}