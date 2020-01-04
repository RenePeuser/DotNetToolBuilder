using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.NewTool;
using FileSystem.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetTool.Builder.App
{
    internal class App
    {
        public IServiceProvider ServiceProvider { get; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<int> RunAsync(string[] args)
        {
            var rootCommand = ServiceProvider.GetService<IDotnetCommandBuilder>().Build();
            var errorHandler = ServiceProvider.GetService<IErrorHandler>();

            var directoryService = ServiceProvider.GetService<IDirectoryService>();
            var currentDirectory = directoryService.GetDirectoryInfo(Environment.CurrentDirectory);
            directoryService.SetCurrentDirectoryInfo(currentDirectory);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            commandLineBuilder.UseMiddleware(errorHandler.HandleErrors);
            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            var option = parser.Configuration.RootCommand.Options.Single(o => o.Name == "version") as Option;
            option?.AddAlias("-v");

            return parser.InvokeAsync(args);
        }
    }
}
