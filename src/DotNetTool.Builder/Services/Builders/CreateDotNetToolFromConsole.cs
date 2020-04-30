using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.InfoCollectors;
using Extensions.Pack;

namespace DotNetTool.Builder.Services.Builders
{
    internal class CreateDotNetToolFromConsole : IBuildDotNetToolStrategy
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