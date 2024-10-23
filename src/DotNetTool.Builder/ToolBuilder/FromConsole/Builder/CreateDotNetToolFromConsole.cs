using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.Services.Builders;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Builder
{
    internal sealed class CreateDotNetToolFromConsole(IDotNetToolInfoCollector dotNetToolInfoCollector) : IBuildDotNetTool
    {
        public Models.DotNetTool BuildFrom(NewToolParameters newToolParameters)
        {
            return dotNetToolInfoCollector.Collect();
        }

        public bool IsThisBuilderFor(NewToolParameters newToolParameters)
        {
            return newToolParameters.FromFile.IsNull();
        }
    }
}