namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
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
            var dotnetToolName = _collectDotNetToolName.Collect(projectName);
            var parameter = _parameterExpressionCollector.CollectFor(dotnetToolName, projectName);

            return new Models.DotNetTool(projectName, dotnetToolName, parameter);
        }
    }
}
