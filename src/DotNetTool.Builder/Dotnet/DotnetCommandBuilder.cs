using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

namespace DotNetTool.Builder.DotNet
{
    
    internal sealed class DotnetCommandBuilder(IEnumerable<IDotnetSubCommandBuilder> dotnetSubCommandBuilders) : IDotnetCommandBuilder
    {
        public RootCommand Build()
        {
            var rootCommand = new RootCommand { Name = "dotnet", Description = @"Run 'dotnet [command] --help' in order to get specific information." };

            dotnetSubCommandBuilders.ToList().ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}
