using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser
{
    public interface IParameterExpressionParser
    {
        ParameterInfo Parse(string paramterExpression, ParameterInfo lastParameter);
    }
}
