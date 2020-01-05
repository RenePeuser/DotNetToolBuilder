namespace DotNetTool.Builder.Validation.Expression
{
    using Models;

    internal interface IExpressionValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}