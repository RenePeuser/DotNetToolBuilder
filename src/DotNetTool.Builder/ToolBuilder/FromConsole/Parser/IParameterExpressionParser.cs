using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Parser
{
    internal interface IParameterExpressionParser
    {
        CommandInfo Parse(ExpressionInfo parameterExpression, CommandInfo previousCommand);
    }
}
