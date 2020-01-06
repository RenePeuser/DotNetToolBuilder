using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandHandlerBuilder
    {
        string Build(CommandInfo parameterInfo);
    }
}
