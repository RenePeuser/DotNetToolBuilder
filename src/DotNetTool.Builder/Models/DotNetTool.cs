using System.Diagnostics;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(DotNetToolName) + "}")]
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
