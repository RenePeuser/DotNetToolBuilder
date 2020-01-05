using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface IParameterExpressionCollector
    {
        CommandInfo CollectFor(string dotnetToolName);
    }
}
