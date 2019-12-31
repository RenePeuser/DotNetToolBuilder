namespace DotNetTool.Builder.Builder.Commands
{
    using Models;

    internal interface ICommandHandlerBuilder
    {
        string Build(CommandInfo parameterInfo);
    }
}