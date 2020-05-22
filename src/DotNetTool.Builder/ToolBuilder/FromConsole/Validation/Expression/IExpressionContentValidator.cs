using DotNetTool.Builder.Models;
using DotNetTool.Builder.Models.Validation;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Validation.Expression
{
    internal interface IExpressionContentValidator
    {
        ValidationResult IsValid(DotNetToolName dotNetDotNetToolName, ExpressionInfo expressionInfo, string projectName);
    }
}
