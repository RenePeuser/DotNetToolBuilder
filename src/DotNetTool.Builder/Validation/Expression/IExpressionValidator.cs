using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Validation.Expression
{
    internal interface IExpressionValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}
