namespace DotNetTool.Builder.Models
{
    public class DotNetTool
    {
        public DotNetTool(string projectName, string toolName, ParameterInfo parameterInfo)
        {
            ProjectName = projectName;
            ToolName = toolName;
            ParameterInfo = parameterInfo;
        }

        public string ProjectName { get; }

        public string ToolName { get; }

        public ParameterInfo ParameterInfo { get; }
    }
}