namespace Trumpf.Hmi.Uif
{
    using System.Collections.Generic;
    using System.CommandLine;
    using Trumpf.Hmi.Extensions;

    public class UifRootCommandBuilder : IUifRootCommandBuilder
    {
        private readonly IEnumerable<ICommandBuilder> _commandBuilders;

        public UifRootCommandBuilder(IEnumerable<ICommandBuilder> commandBuilders)
        {
            _commandBuilders = commandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = "uif",
                Description = @"Run 'uif [command] --help' in order to get more information for a command.",
            };

            _commandBuilders.ForEach(builder => rootCommand.AddCommand(builder.Build()));

            return rootCommand;
        }
    }
}