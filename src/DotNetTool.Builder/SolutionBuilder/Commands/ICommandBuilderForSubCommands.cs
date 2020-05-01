using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface ICommandBuilderForSubCommands
    {
        string Build(string project, CommandInfo parameterInfo, CommandInfo parent, string nameSpace);
    }
}
