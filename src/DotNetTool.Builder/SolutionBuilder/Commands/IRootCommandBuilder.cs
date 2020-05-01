using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface IRootCommandBuilder
    {
        string Build(string project, CommandInfo parameterInfo, string nameSpace);
    }
}
