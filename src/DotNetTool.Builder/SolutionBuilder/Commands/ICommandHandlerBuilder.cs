using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface ICommandHandlerBuilder
    {
        string Build(CommandInfo parameterInfo);
    }
}
