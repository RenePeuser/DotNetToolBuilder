namespace Trumpf.Hmi.Uif
{
    using System.Collections.Generic;
    using System.CommandLine;
    using Trumpf.Hmi.Extensions;

    public class UifCommandBuilder : IUifCommandBuilder
    {
        private readonly IEnumerable<IUifSubCommandBuilder> _ultraSubCommandBuilders;

        public UifCommandBuilder(IEnumerable<IUifSubCommandBuilder> ultraSubCommandBuilders)
        {
            _ultraSubCommandBuilders = ultraSubCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = "ultra",
                Description = @"Run 'ultra [command] --help' in order to get specific information.",
            };

            _ultraSubCommandBuilders.ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}