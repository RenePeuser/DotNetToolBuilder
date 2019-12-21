using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser
{
    public interface IParameterExpressionParser
    {
        CliParameterInfo Parse(string paramterExpression, CliParameterInfo lastParameter);
    }
}