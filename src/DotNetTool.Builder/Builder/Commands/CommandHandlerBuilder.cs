namespace DotNetTool.Builder.Builder.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    internal class CommandHandlerBuilder : ICommandHandlerBuilder
    {
        private readonly IEnumerable<ICommandHandlerStringBuilder> _commandHandlerStringBuilders;

        public CommandHandlerBuilder(IEnumerable<ICommandHandlerStringBuilder> commandHandlerStringBuilders)
        {
            _commandHandlerStringBuilders = commandHandlerStringBuilders;
        }

        public string Build(ParameterInfo parameterInfo)
        {
            var builder = _commandHandlerStringBuilders.Single(builder => builder.IsThisBuilderFor(parameterInfo));
            return builder.Build(parameterInfo);
        }
    }
}