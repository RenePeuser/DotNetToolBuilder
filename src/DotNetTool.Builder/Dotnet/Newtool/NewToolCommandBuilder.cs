using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using DotNetTool.Builder.DotNet.Newtool.Options;
using DotNetTool.Builder.DotNet.Newtool.Service;

namespace DotNetTool.Builder.DotNet.Newtool
{
    internal class NewToolCommandBuilder : IDotnetSubCommandBuilder
    {
        private readonly INewToolService _newToolService;
        private readonly INewToolOptionsBuilder _optionsBuilder;

        public NewToolCommandBuilder(INewToolService newToolService, INewToolOptionsBuilder optionsBuilder)
        {
            _newToolService = newToolService;
            _optionsBuilder = optionsBuilder;
        }

        public Command Build()
        {
            var command = new Command("newtool", "creates a new dotnet tool");
            _optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.Handler = CommandHandler.Create<FileInfo, DirectoryInfo, bool, bool, bool, bool>((fromFile, saveTo, useCode, usevisualstudio, useRider, asZip) => _newToolService.HandleAsync(new NewToolParameters(fromFile, saveTo, useCode, usevisualstudio, useRider, asZip)));
            return command;
        }
    }
}
