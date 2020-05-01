using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface ICommandServiceInterfaceBuilder
    {
        string Build(string project, CommandInfo parameterInfo, string nameSpace);
    }
}
