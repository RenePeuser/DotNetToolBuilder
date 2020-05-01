namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class DotNetToolOptimizer
    {
        private readonly DotNetToolNameOptimizer _dotNetToolNameOptimizer;
        private readonly CommandInfoOptimizer _commandInfoOptimizer;

        public DotNetToolOptimizer(DotNetToolNameOptimizer dotNetToolNameOptimizer, CommandInfoOptimizer commandInfoOptimizer)
        {
            _dotNetToolNameOptimizer = dotNetToolNameOptimizer;
            _commandInfoOptimizer = commandInfoOptimizer;
        }

        internal Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var optmizedToolName = _dotNetToolNameOptimizer.Optimize(dotNetTool.DotNetToolName);
            var optimizedCommand = _commandInfoOptimizer.Optimize(dotNetTool.ParameterInfo);

            return new Models.DotNetTool(dotNetTool.ProjectName, optmizedToolName, optimizedCommand);
        }
    }
}
