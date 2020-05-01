using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface ICommandServiceBuilder
    {
        string Build(string project, CommandInfo parameterInfo, string nameSpace);
    }
}
