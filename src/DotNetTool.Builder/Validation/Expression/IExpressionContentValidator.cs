namespace DotNetTool.Builder.Validation.Expression
{
    using Models;

    internal interface IExpressionContentValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}