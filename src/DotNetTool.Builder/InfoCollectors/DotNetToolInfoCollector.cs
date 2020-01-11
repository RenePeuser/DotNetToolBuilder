using DotNetTool.Builder.Services.Collectors;

namespace DotNetTool.Builder.InfoCollectors
{
    internal class DotNetToolInfoCollector : IDotNetToolInfoCollector
    {
        private readonly ICollectDotNetToolName _collectDotNetToolName;
        private readonly ICollectProjectName _collectProjectName;
        private readonly IParameterExpressionCollector _parameterExpressionCollector;

        public DotNetToolInfoCollector(ICollectProjectName collectProjectName, ICollectDotNetToolName collectDotNetToolName, IParameterExpressionCollector parameterExpressionCollector)
        {
            _collectProjectName = collectProjectName;
            _collectDotNetToolName = collectDotNetToolName;
            _parameterExpressionCollector = parameterExpressionCollector;
        }

        public Models.DotNetTool Collect()
        {
            var projectName = _collectProjectName.Collect();
            var dotnetToolName = _collectDotNetToolName.Collect();
            var parameter = _parameterExpressionCollector.CollectFor(dotnetToolName);

            return new Models.DotNetTool(projectName, dotnetToolName, parameter);
        }
    }
}
