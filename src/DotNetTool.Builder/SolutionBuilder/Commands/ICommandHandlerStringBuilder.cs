using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal interface ICommandHandlerStringBuilder
    {
        string Build(CommandInfo parameterInfo);
        bool IsThisBuilderFor(CommandInfo parameterInfo);
    }
}
