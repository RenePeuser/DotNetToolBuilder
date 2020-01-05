using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser
{
    internal interface IParameterExpressionParser
    {
        CommandInfo Parse(ExpressionInfo parameterExpression, CommandInfo previousCommand);
    }
}
