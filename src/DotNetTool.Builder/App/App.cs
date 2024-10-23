using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.DotNet;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.Services.DotNet;
using FileSystem.Abstraction;
using Microsoft.Extensions.DependencyInjection;


namespace DotNetTool.Builder.App
{
    internal sealed class App(IServiceProvider serviceProvider)
    {
        public Task<int> RunAsync(string[] args)
        {
            Throw.IfNull(() => args);

            var rootCommand = serviceProvider.GetService<IDotnetCommandBuilder>().Build();
            var errorHandler = serviceProvider.GetService<IErrorHandler>();
            var dotNetCliArgumentFixer = serviceProvider.GetService<IDotNetCliArgumentFixer>();

            var directoryService = serviceProvider.GetService<IDirectoryService>();
            var currentDirectory = directoryService.GetDirectoryInfo(Environment.CurrentDirectory);
            directoryService.SetCurrentDirectoryInfo(currentDirectory);

            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            commandLineBuilder.UseMiddleware(errorHandler.HandleErrors);
            commandLineBuilder.UseDefaults();
            var parser = commandLineBuilder.Build();

            var option = parser.Configuration.RootCommand.Options.Single(o => o.Name == "version") as Option;
            option?.AddAlias("-v");

            var fixedArgs = dotNetCliArgumentFixer.Fix(args);
            return parser.InvokeAsync(fixedArgs);
        }
    }
}
