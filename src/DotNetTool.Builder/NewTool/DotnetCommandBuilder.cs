using System.CommandLine;
using System.CommandLine.Invocation;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.NewTool.Arguments;
using DotNetTool.Builder.NewTool.Options;
using DotNetTool.Builder.NewTool.Service;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.NewTool
{
    internal class DotnetCommandBuilder : IDotnetCommandBuilder
    {
        private readonly INewToolService _newToolService;
        private readonly INewToolOptionsBuilder _newToolOptionsBuilder;
        private readonly INewToolArgumentBuilder _newToolArgumentBuilder;

        public DotnetCommandBuilder(INewToolService newToolService, INewToolOptionsBuilder newToolOptionsBuilder, INewToolArgumentBuilder newToolArgumentBuilder)
        {
            _newToolService = newToolService;
            _newToolOptionsBuilder = newToolOptionsBuilder;
            _newToolArgumentBuilder = newToolArgumentBuilder;
        }
        public RootCommand Build()
        {
            var rootCommand = new RootCommand();
            rootCommand.Name = "newtool";
            rootCommand.Description = "Creates a new dotnet tool";

            _newToolOptionsBuilder.Build().ForEach(option => rootCommand.AddOption(option));
            rootCommand.AddArgument(_newToolArgumentBuilder.Build());
            rootCommand.Handler = CommandHandler.Create<System.IO.FileInfo, bool>((file, novisualstudio) => _newToolService.HandleAsync(new NewToolParameters(file, novisualstudio)));
            return rootCommand;
        }
    }
}