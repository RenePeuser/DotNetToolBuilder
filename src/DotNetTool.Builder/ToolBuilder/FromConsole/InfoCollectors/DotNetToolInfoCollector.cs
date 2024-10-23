namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal sealed class DotNetToolInfoCollector(ICollectProjectName collectProjectName,
                                                  ICollectDotNetToolName collectDotNetToolName,
                                                  IParameterExpressionCollector parameterExpressionCollector) : IDotNetToolInfoCollector
    {
        public Models.DotNetTool Collect()
        {
            var projectName = collectProjectName.Collect();
            var dotnetToolName = collectDotNetToolName.Collect(projectName);
            var parameter = parameterExpressionCollector.CollectFor(dotnetToolName, projectName);

            return new Models.DotNetTool(projectName, dotnetToolName, parameter);
        }
    }
}
