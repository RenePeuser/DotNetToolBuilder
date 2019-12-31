using System.Diagnostics;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(ToolName) + "}")]
    public class DotNetTool
    {
        public DotNetTool(string projectName, string toolName, CommandInfo parameterInfo)
        {
            ProjectName = projectName;
            ToolName = toolName;
            NormalizedToolName = toolName.FirstCharToUpper();
            ParameterInfo = parameterInfo;
        }

        public string ProjectName { get; }

        public string ToolName { get; }

        public string NormalizedToolName { get; }

        public CommandInfo ParameterInfo { get; }
    }
}
