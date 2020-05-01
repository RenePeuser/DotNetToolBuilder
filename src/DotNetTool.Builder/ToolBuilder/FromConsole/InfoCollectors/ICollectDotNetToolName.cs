using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal interface ICollectDotNetToolName
    {
        DotNetToolName Collect();
    }
}
