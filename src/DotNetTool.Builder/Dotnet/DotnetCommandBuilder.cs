using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

namespace DotNetTool.Builder.Dotnet
{
    internal class DotnetCommandBuilder : IDotnetCommandBuilder
    {
        private readonly IEnumerable<IDotnetSubCommandBuilder> _dotnetSubCommandBuilders;

        public DotnetCommandBuilder(IEnumerable<IDotnetSubCommandBuilder> dotnetSubCommandBuilders)
        {
            _dotnetSubCommandBuilders = dotnetSubCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand { Name = "dotnet", Description = @"Run 'dotnet [command] --help' in order to get specific information." };

            _dotnetSubCommandBuilders.ToList().ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}
