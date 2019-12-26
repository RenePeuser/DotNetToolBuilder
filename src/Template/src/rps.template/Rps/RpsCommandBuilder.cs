using System.Linq;

namespace rps.template
{
    using System.Collections.Generic;
    using System.CommandLine;

    public class RpsCommandBuilder : IRpsCommandBuilder
    {
        private readonly IEnumerable<IRpsSubCommandBuilder> _rpsSubCommandBuilders;

        public RpsCommandBuilder(IEnumerable<IRpsSubCommandBuilder> rpsSubCommandBuilders)
        {
            _rpsSubCommandBuilders = rpsSubCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = "rps",
                Description = @"Run 'rps [command] --help' in order to get specific information.",
            };

            _rpsSubCommandBuilders.ToList().ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}