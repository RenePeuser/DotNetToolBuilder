namespace Trumpf.Hmi.Uif
{
    using System.Collections.Generic;
    using System.CommandLine;
    using Trumpf.Hmi.Extensions;

    public class UltraCommandBuilder : IUltraCommandBuilder
    {
        private readonly IEnumerable<IUltraSubCommandBuilder> _ultraSubCommandBuilders;

        public UltraCommandBuilder(IEnumerable<IUltraSubCommandBuilder> ultraSubCommandBuilders)
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