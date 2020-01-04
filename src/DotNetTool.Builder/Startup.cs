using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;
using Microsoft.Extensions.DependencyInjection;
using DotNetTool.Builder.Parser.Commands;
using DotNetTool.Builder.Tokenizer;
using FileSystem.Abstraction;

namespace DotNetTool.Builder
{
    internal class Startup
    {
        internal void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);
            RegisterValidation(services);
            
            RegisterTokenizer(services);
            RegisterArgumentParser(services);
            RegisterOptionsParser(services);
            RegisterParameterParser(services);

            RegisterDotNetToolInfoCollector(services);

            RegisterArgumentBuilder(services);
            RegisterOptionsBuilder(services);
            RegisterParameterClassBuilder(services);
            RegisterCommandBuilders(services);
            RegisterStartUpBuilder(services);
        }

        private void RegisterTokenizer(IServiceCollection services)
        {
            services.AddSingleton<IExpressionTokenizer, ExpressionTokenizer>();

            services.AddSingleton<ITokenizer, ArgumentTokenizer>();
            services.AddSingleton<ITokenizer, OptionTokenizer>();
            services.AddSingleton<ITokenizer, CommandTokenizer>();
        }

        private void RegisterValidation(IServiceCollection services)
        {
            services.AddSingleton<IProjectNameValidator, ProjectNameValidator>();
            services.AddSingleton<IToolNameValidator, Validation.ToolNameValidator>();

            services.AddSingleton<IPrimitiveTypeNameValidator, PrimitiveTypeNameValidator>();

            services.AddSingleton<IExpressionValidator, ExpressionValidator>();
            services.AddSingleton<IExpressionContentValidator, TypeCastValidator>();
            services.AddSingleton<IExpressionContentValidator, ArgumentValidator>();
            services.AddSingleton<IExpressionContentValidator, OptionValidator>();
            services.AddSingleton<IExpressionContentValidator, CharValidator>();
            services.AddSingleton<IExpressionContentValidator, Validation.Expression.ToolNameValidator>();
            services.AddSingleton<IExpressionContentValidator, MinimumCommandValidator>();
            services.AddSingleton<IExpressionContentValidator, OnlyOneArgumentValidator>();
            services.AddSingleton<IExpressionContentValidator, CommandMustBeforeOptionOrArgumentValidator>();
            services.AddSingleton<IExpressionContentValidator, CommandNameValidation>();
            services.AddSingleton<IExpressionContentValidator, UnknownTokenValidator>();
            services.AddSingleton<IExpressionContentValidator, MultipleOptionValidator>();
        }

        private void RegisterStartUpBuilder(IServiceCollection services)
        {
            services.AddSingleton<IStartUpBuilder, StartUpBuilder>();
            services.AddSingleton<IRegisterServiceMethodBuilder, RegisterServiceMethodBuilder>();
            services.AddSingleton<ITypeRegistrationBuilder, TypeRegistrationBuilder>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ITemplateExtractor, TemplateExtractor>();
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IParameterService, ParameterService>();
            services.AddSingleton<ICommandTypeCollector, CommandTypeCollector>();
            services.AddSingleton<ICreateCommandClasses, CreateCommandClasses>();
            services.AddSingleton<IProcessBuilder, ProcessBuilder>();
            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<INameSpaceCollector, NameSpaceCollector>();
            services.AddSingleton<IRenameFilesAndFolders, RenameFilesAndFolders>();
            services.AddSingleton<ITemplateService, TemplateService>();
            services.AddSingleton<IVisualStudioService, VisualStudioService>();
            services.AddSingleton<IDotNetToolTestService, DotNetToolTestService>();
            services.AddSingleton<IDotNetToolSerializer, DotNetToolSerializer>();
            services.AddSingleton<ITargetFolderService, TargetFolderService>();
        }

        internal void RegisterDotNetToolInfoCollector(IServiceCollection services)
        {
            services.AddSingleton<IDotNetToolInfoCollector, DotNetToolInfoCollector>();
            services.AddSingleton<ICollectProjectName, CollectProjectName>();
            services.AddSingleton<ICollectDotNetToolName, CollectDotNetToolName>();
            services.AddSingleton<IParameterExpressionCollector, ParameterExpressionCollector>();
        }

        internal void RegisterArgumentParser(IServiceCollection services)
        {
            services.AddSingleton<IArgumentParser, ArgumentParser>();
        }

        internal void RegisterOptionsParser(IServiceCollection services)
        {
            services.AddSingleton<IOptionParser, OptionParser>();
        }

        internal void RegisterParameterParser(IServiceCollection services)
        {
            services.AddSingleton<ICommandParser, CommandParser>();
            services.AddSingleton<IParameterExpressionParser, ParameterExpressionParser>();
        }

        internal void RegisterArgumentBuilder(IServiceCollection services)
        {
            services.AddSingleton<IArgumentBuilder, ArgumentBuilder>();
            services.AddSingleton<IArgumentInterfaceBuilder, ArgumentInterfaceBuilder>();
        }

        internal void RegisterOptionsBuilder(IServiceCollection services)
        {
            services.AddSingleton<IOptionInterfaceBuilder, OptionInterfaceBuilder>();
            services.AddSingleton<IOptionImplementationBuilder, OptionImplementationBuilder>();
            services.AddSingleton<IOptionMethodsBuilder, OptionMethodsBuilder>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithArgument>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithoutArgument>();
            services.AddSingleton<INewOptionExpressionService, NewOptionExpressionService>();
        }

        internal void RegisterParameterClassBuilder(IServiceCollection services)
        {
            services.AddSingleton<IParameterClassBuilder, ParameterClassBuilder>();
            services.AddSingleton<IParameterSpecificClassBuilder, ParameterWithoutArgsOrOptionsClassBuilder>();
            services.AddSingleton<IParameterSpecificClassBuilder, ParameterWithArgsOrOptionsClassBuilder>();
        }

        internal void RegisterCommandBuilders(IServiceCollection services)
        {
            services.AddSingleton<ICommandBuilderForSubCommands, CommandBuilderForSubCommands>();
            services.AddSingleton<ICommandBuilderSimple, CommandBuilderSimple>();
            services.AddSingleton<ICommandBuilderWithArgument, CommandBuilderWithArgument>();
            services.AddSingleton<ICommandBuilderWithArgumentAndOption, CommandBuilderWithArgumentAndOption>();
            services.AddSingleton<ICommandBuilderWithOptions, CommandBuilderWithOptions>();

            services.AddSingleton<ICommandServiceInterfaceBuilder, CommandServiceInterfaceBuilder>();
            services.AddSingleton<IRootCommandBuilder, RootCommandBuilder>();
            services.AddSingleton<IRootCommandInterfaceBuilder, RootCommandInterfaceBuilder>();
            services.AddSingleton<ISubCommandInterfaceBuilder, SubCommandInterfaceBuilder>();
            services.AddSingleton<ICommandServiceBuilder, CommandServiceBuilder>();


            services.AddSingleton<ICommandHandlerBuilder, CommandHandlerBuilder>();
            services.AddSingleton<ICommandHandlerStringBuilder, CommandHandlerWithArgsOrOptionBuilder>();
            services.AddSingleton<ICommandHandlerStringBuilder, CommandHandlerNoArgsAndNoOptionBuilder>();
        }
    }
}
