namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class DotNetToolOptimizer(DotNetToolNameOptimizer dotNetToolNameOptimizer,
                                              CommandInfoOptimizer commandInfoOptimizer)
    {
        internal Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var optmizedToolName = dotNetToolNameOptimizer.Optimize(dotNetTool.DotNetToolName);
            var optimizedCommand = commandInfoOptimizer.Optimize(dotNetTool.ParameterInfo);

            return new Models.DotNetTool(dotNetTool.ProjectName, optmizedToolName, optimizedCommand);
        }
    }
}
