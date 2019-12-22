using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    public class DotNetToolInfoCollector : IDotNetToolInfoCollector
    {
        private readonly ICollectDotNetToolName _collectDotNetToolName;
        private readonly ICollectProjectName _collectProjectName;
        private readonly IParameterExpressionCollector _parameterExpressionCollector;

        public DotNetToolInfoCollector(ICollectProjectName collectProjectName,
            ICollectDotNetToolName collectDotNetToolName, IParameterExpressionCollector parameterExpressionCollector)
        {
            _collectProjectName = collectProjectName;
            _collectDotNetToolName = collectDotNetToolName;
            _parameterExpressionCollector = parameterExpressionCollector;
        }

        public Models.DotNetTool Collect()
        {
            var projectName = _collectProjectName.Invoke();
            var dotnetToolName = _collectDotNetToolName.Invoke();
            var parameter = _parameterExpressionCollector.Collect();

            return new Models.DotNetTool(projectName, dotnetToolName, parameter);
        }
    }
}