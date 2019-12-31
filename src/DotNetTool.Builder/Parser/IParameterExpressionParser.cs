using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser
{
    public interface IParameterExpressionParser
    {
        CommandInfo Parse(ExpressionInfo parameterExpression, CommandInfo previousCommand);
    }
}
