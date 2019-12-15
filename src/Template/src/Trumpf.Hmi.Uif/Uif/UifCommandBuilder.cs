namespace Trumpf.Hmi.Uif
{
    using System.Collections.Generic;
    using System.CommandLine;
    using Trumpf.Hmi.Extensions;

    public class UifCommandBuilder : IUifCommandBuilder
    {
        private readonly IEnumerable<IUifCommandBuilder> _uifCommandBuilders;

        public UifCommandBuilder(IEnumerable<IUifCommandBuilder> uifCommandBuilders)
        {
            _uifCommandBuilders = uifCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = "uif",
                Description = @"Run 'uif [command] --help' in order to get specific information.",
            };

            _uifCommandBuilders.ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}