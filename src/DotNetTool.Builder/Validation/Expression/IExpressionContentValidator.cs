using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Validation.Expression
{
    internal interface IExpressionContentValidator
    {
        ValidationResult IsValid(string dotNetToolName, ExpressionInfo expressionInfo);
    }
}
