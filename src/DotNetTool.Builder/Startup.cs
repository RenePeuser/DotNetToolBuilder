using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Parser.Parameters;
using DotNetTool.Builder.Services;
using Microsoft.Extensions.DependencyInjection;
using Trumpf.Hmi.FileSystemAbstraction.Services;

namespace DotNetTool.Builder
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<TiFileService, TcFileService>();

            RegisterArgumentParser(services);
            RegisterOptionsParser(services);
            RegisterParameterParser(services);
            RegisterParameterValueParser(services);

            RegisterDotNetToolInfoCollector(services);

            RegisterArgumentBuilder(services);
            RegisterOptionsBuilder(services);
            RegisterParameterClassBuilder(services);
            RegisterCommandBuilders(services);
        }

        public void RegisterDotNetToolInfoCollector(IServiceCollection services)
        {
            services.AddSingleton<IDotNetToolInfoCollector, DotNetToolInfoCollector>();
            services.AddSingleton<ICollectProjectName, CollectProjectName>();
            services.AddSingleton<ICollectDotNetToolName, CollectDotNetToolName>();
            services.AddSingleton<IParameterExpressionCollector, ParameterExpressionCollector>();
        }

        public void RegisterArgumentParser(IServiceCollection services)
        {
            services.AddSingleton<IArgumentOnly, ArgumentOnly>();

            services.AddSingleton<IArgumentParser, ArgumentOnly>();
            services.AddSingleton<IArgumentParser, ArgumentWithPostTypeCast>();
            services.AddSingleton<IArgumentParser, ArgumentWithPreTypeCast>();
        }

        public void RegisterOptionsParser(IServiceCollection services)
        {
            services.AddSingleton<IOptionParser, OptionParser>();
        }

        public void RegisterParameterParser(IServiceCollection services)
        {
            services.AddSingleton<IParameterParser, ParameterParser>();
            services.AddSingleton<IParameterExpressionParser, ParameterExpressionParser>();
        }

        public void RegisterParameterValueParser(IServiceCollection services)
        {
            services.AddSingleton<IParameterValueParser, ArgumentOnly>();
            services.AddSingleton<IParameterValueParser, ArgumentWithPostTypeCast>();
            services.AddSingleton<IParameterValueParser, ArgumentWithPreTypeCast>();
            services.AddSingleton<IParameterValueParser, OptionParser>();
            services.AddSingleton<IParameterValueParser, ParameterParser>();
        }

        public void RegisterArgumentBuilder(IServiceCollection services)
        {
            services.AddSingleton<IArgumentBuilder, ArgumentBuilder>();
        }

        public void RegisterOptionsBuilder(IServiceCollection services)
        {
            services.AddSingleton<IOptionInterfaceBuilder, OptionInterfaceBuilder>();
            services.AddSingleton<IOptionImplementationBuilder, OptionImplementationBuilder>();
        }

        public void RegisterParameterClassBuilder(IServiceCollection services)
        {
            services.AddSingleton<IParameterClassBuilder, ParameterClassBuilder>();
        }

        public void RegisterCommandBuilders(IServiceCollection services)
        {
            services.AddSingleton<ICommandBuilderForSubCommands, CommandBuilderForSubCommands>();
            services.AddSingleton<ICommandBuilderSimple, CommandBuilderSimple>();
            services.AddSingleton<ICommandBuilderWithArgument, CommandBuilderWithArgument>();
            services.AddSingleton<ICommandBuilderWithArgumentAndOption, CommandBuilderWithArgumentAndOption>();
            services.AddSingleton<ICommandBuilderWithOptions, CommandBuilderWithOptions>();
            services.AddSingleton<ICommandHandlerStringBuilder, CommandHandlerStringBuilder>();
            services.AddSingleton<ICommandInterfaceBuilder, CommandInterfaceBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
            services.AddSingleton<IRootCommandInterfaceBuilder, RootCommandInterfaceBuilder>();
            services.AddSingleton<ISubCommandInterfaceBuilder, SubCommandInterfaceBuilder>();
        }
    }
}
