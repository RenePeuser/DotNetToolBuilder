using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using DotNetTool.Builder.Dotnet.Newtool.Options;
using DotNetTool.Builder.Dotnet.Newtool.Service;

namespace DotNetTool.Builder.Dotnet.Newtool
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
            command.Handler = CommandHandler.Create<System.IO.FileInfo, System.IO.DirectoryInfo, bool, bool, bool>((fromFile, saveTo, useCode, usevisualstudio, useRider) => _newToolService.HandleAsync(new NewToolParameters(fromFile, saveTo, useCode, usevisualstudio, useRider)));
            return command;
        }
    }
}