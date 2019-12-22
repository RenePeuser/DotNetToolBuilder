namespace DotNetTool.Builder.Models
{
    public class DotNetTool
    {
        public DotNetTool(string projectName, string toolName, ParameterInfo parameterInfo)
        {
            ProjectName = projectName;
            ToolName = toolName;
            NormalizedToolName = NormalizedToolName;
            ParameterInfo = parameterInfo;
        }

        public string ProjectName { get; }

        public string ToolName { get; }

        public string NormalizedToolName { get; }

        public ParameterInfo ParameterInfo { get; }
    }
}