using System.Collections.Generic;
using System.Linq;
using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
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

            var builder = _commandHandlerStringBuilders.Single(b => b.IsThisBuilderFor(parameterInfo));
            return builder.Build(parameterInfo);
        }
    }
}
