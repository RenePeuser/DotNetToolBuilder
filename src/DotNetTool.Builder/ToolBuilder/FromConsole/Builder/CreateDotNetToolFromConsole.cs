using DotNetTool.Builder.DotNet.Newtool;
using DotNetTool.Builder.Services.Builders;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.Builder
{
    internal class CreateDotNetToolFromConsole : IBuildDotNetTool
    {
        private readonly IDotNetToolInfoCollector _dotNetToolInfoCollector;

        public CreateDotNetToolFromConsole(IDotNetToolInfoCollector dotNetToolInfoCollector)
        {
            _dotNetToolInfoCollector = dotNetToolInfoCollector;
        }

        public Models.DotNetTool BuildFrom(NewToolParameters newToolParameters)
        {
            return _dotNetToolInfoCollector.Collect();
        }

        public bool IsThisBuilderFor(NewToolParameters newToolParameters)
        {
            return newToolParameters.FromFile.IsNull();
        }
    }
}