using DotNetTool.Builder.App;
using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.FileSystemAbstraction.Services;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Parser.Parameters;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetTool.Builder
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);
            RegisterValidation(services);

            RegisterArgumentParser(services);
            RegisterOptionsParser(services);
            RegisterParameterParser(services);
            RegisterParameterValueParser(services);

            RegisterDotNetToolInfoCollector(services);

            RegisterArgumentBuilder(services);
            RegisterOptionsBuilder(services);
            RegisterParameterClassBuilder(services);
            RegisterCommandBuilders(services);
            RegisterStartUpBuilder(services);
        }

        private void RegisterValidation(IServiceCollection services)
        {
            services.AddSingleton<IProjectNameValidator, ProjectNameValidator>();
            services.AddSingleton<IToolNameValidator, ToolNameValidator>();

            services.AddSingleton<IExpressionValidator, ExpressionValidator>();
            services.AddSingleton<IExpressionContentValidator, ExpressionCastValidator>();
            services.AddSingleton<IExpressionContentValidator, ExpressionArgumentValidator>();
            services.AddSingleton<IExpressionContentValidator, ExpressionOptionValidator>();
            services.AddSingleton<IExpressionContentValidator, ExpressionCharValidator>();
            services.AddSingleton<IExpressionContentValidator, ExpressionToolNameValidator>();

        }

        private void RegisterStartUpBuilder(IServiceCollection services)
        {
            services.AddSingleton<IStartUpBuilder, StartUpBuilder>();
            services.AddSingleton<IRegisterServiceMethodBuilder, RegisterServiceMethodBuilder>();
            services.AddSingleton<ITypeRegistrationBuilder, TypeRegistrationBuilder>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IExtractTemplate, ExtractTemplate>();
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IParameterService, ParameterService>();
            services.AddSingleton<ICommandTypeCollector, CommandTypeCollector>();
            services.AddSingleton<ICreateCommandClasses, CreateCommandClasses>();
            services.AddSingleton<IDotNetToolService, DotNetToolService>();
            services.AddSingleton<IDotNetToolSynchronizer, DotNetToolSynchronizer>();
            services.AddSingleton<IProcessBuilder, ProcessBuilder>();
            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<INameSpaceCollector, NameSpaceCollector>();
            services.AddSingleton<IRenameFilesAndFolders, RenameFilesAndFolders>();
            services.AddSingleton<IVisualStudioService, VisualStudioService>();
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
            services.AddSingleton<IArgumentInterfaceBuilder, ArgumentInterfaceBuilder>();
        }

        public void RegisterOptionsBuilder(IServiceCollection services)
        {
            services.AddSingleton<IOptionInterfaceBuilder, OptionInterfaceBuilder>();
            services.AddSingleton<IOptionImplementationBuilder, OptionImplementationBuilder>();
            services.AddSingleton<IOptionMethodsBuilder, OptionMethodsBuilder>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithArgument>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithoutArgument>();
            services.AddSingleton<INewOptionExpressionService, NewOptionExpressionService>();
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
            services.AddSingleton<ICommandServiceBuilder, CommandServiceBuilder>();
        }
    }
}
