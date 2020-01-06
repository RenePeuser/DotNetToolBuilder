using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services.Collectors
{
    internal interface IParameterExpressionCollector
    {
        CommandInfo CollectFor(string dotnetToolName);
    }
}
