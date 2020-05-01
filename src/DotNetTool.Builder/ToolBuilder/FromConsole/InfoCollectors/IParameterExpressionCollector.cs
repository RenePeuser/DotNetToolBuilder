using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal interface IParameterExpressionCollector
    {
        CommandInfo CollectFor(DotNetToolName dotnetDotNetToolName);
    }
}
