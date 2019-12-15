namespace Trumpf.Hmi.Uif
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Invocation;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Trumpf.Hmi.Extensions;
    using Trumpf.Hmi.Uif.ErrorHandling;

    public class App
    {
        public IServiceProvider ServiceProvider { get; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<int> RunAsync(string[] args)
        {
            var rootCommand = ServiceProvider.GetService<IUifCommandBuilder>().Build();
            var errorHandler = ServiceProvider.GetService<IErrorHandler>();

            var commandLineBuilder = new CommandLineBuilder(rootCommand);

            commandLineBuilder.UseMiddleware(errorHandler.HandleErrors);
            commandLineBuilder.UseDefaults();

            var parser = commandLineBuilder.Build();

            parser.Configuration.RootCommand.Options.Single(o => o.Name == "version").As<Option>().AddAlias("-v");

            return parser.InvokeAsync(args);
        }
    }
}