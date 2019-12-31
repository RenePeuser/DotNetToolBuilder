namespace DotNetTool.Builder.Builder.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using global::Argument.Check;
    using Models;

    internal class CommandHandlerBuilder : ICommandHandlerBuilder
    {
        private readonly IEnumerable<ICommandHandlerStringBuilder> _commandHandlerStringBuilders;

        public CommandHandlerBuilder(IEnumerable<ICommandHandlerStringBuilder> commandHandlerStringBuilders)
        {
            Throw.IfNullOrEmpty(() => commandHandlerStringBuilders);

            _commandHandlerStringBuilders = commandHandlerStringBuilders;
        }

        public string Build(CommandInfo parameterInfo)
        {
            Throw.IfNull(() => parameterInfo);

            var builder = _commandHandlerStringBuilders.Single(builder => builder.IsThisBuilderFor(parameterInfo));
            return builder.Build(parameterInfo);
        }
    }
}