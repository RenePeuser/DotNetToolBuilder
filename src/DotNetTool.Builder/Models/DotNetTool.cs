using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("Project: {" + nameof(ProjectName) + "} ToolName: {" + nameof(Models.DotNetToolName.Name) + "}")]
    internal class DotNetTool
    {
        public DotNetTool(string projectName, DotNetToolName dotNetToolName, CommandInfo parameterInfo)
        {
            ProjectName = projectName;
            DotNetToolName = dotNetToolName;
            ParameterInfo = parameterInfo;
        }

        public string ProjectName { get; }

        public DotNetToolName DotNetToolName { get; }

        public CommandInfo ParameterInfo { get; }
    }
}
