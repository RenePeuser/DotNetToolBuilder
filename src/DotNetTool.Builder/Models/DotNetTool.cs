namespace DotNetTool.Builder.Models
{
    public class DotNetTool
    {
        public DotNetTool(string projectName, string toolName, CliParameterInfo cliParameterInfo)
        {
            ProjectName = projectName;
            ToolName = toolName;
            CliParameterInfo = cliParameterInfo;
        }

        public string ProjectName { get; }

        public string ToolName { get; }

        public CliParameterInfo CliParameterInfo { get; }
    }
}