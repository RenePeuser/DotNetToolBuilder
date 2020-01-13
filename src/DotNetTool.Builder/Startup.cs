using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Builder.FileStructure;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.Dotnet;
using DotNetTool.Builder.Dotnet.Newtool;
using DotNetTool.Builder.Dotnet.Newtool.Options;
using DotNetTool.Builder.Dotnet.Newtool.Service;
using DotNetTool.Builder.ErrorHandling;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Parser;
using DotNetTool.Builder.Parser.Argument;
using DotNetTool.Builder.Parser.Commands;
using DotNetTool.Builder.Parser.Options;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Collectors;
using DotNetTool.Builder.Services.DotNet;
using DotNetTool.Builder.Services.IDE;
using DotNetTool.Builder.Services.IO;
using DotNetTool.Builder.Services.Optimizer;
using DotNetTool.Builder.Services.Template;
using DotNetTool.Builder.Tokenizer;
using DotNetTool.Builder.Validation;
using DotNetTool.Builder.Validation.Expression;
using FileSystem.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using ToolNameValidator = DotNetTool.Builder.Validation.ToolNameValidator;

namespace DotNetTool.Builder
{
    internal class Startup
    {
        internal void ConfigureServices(IServiceCollection services)
        {
            RegisterCli(services);

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
            RegisterFileStructureCreators(services);
            RegisterArgumentTypeOptimizer(services);
        }

        private void RegisterArgumentTypeOptimizer(IServiceCollection services)
        {
            services.AddSingleton<IArgumentTypeOptimizer, ArgumentTypeOptimizer>();
            services.AddSingleton<ITypeNameOptimizer, FileInfoOptimizer>();
            services.AddSingleton<ITypeNameOptimizer, DirectoryInfoOptimizer>();
            services.AddSingleton<ITypeNameOptimizer, FileSystemInfoOptimizer>();
            services.AddSingleton<ITypeNameOptimizer, SystemTypeNameOptimizer>();

            services.AddSingleton<IDotNetToolNameNormalizer, DotNetToolNameNormalizer>();
        }

        private void RegisterCli(IServiceCollection services)
        {
            services.AddSingleton<IDotNetCliArgumentFixer, DotNetCliArgumentFixer>();

            services.AddSingleton<IErrorHandler, ErrorHandler>();
            services.AddSingleton<IDotnetCommandBuilder, DotnetCommandBuilder>();
            services.AddSingleton<INewToolOptionsBuilder, NewToolOptionsBuilder>();
            services.AddSingleton<IDotnetSubCommandBuilder, NewToolCommandBuilder>();
            services.AddSingleton<INewToolService, NewToolService>();
        }

        private void RegisterFileStructureCreators(IServiceCollection services)
        {
            services.AddSingleton<IBuildCommandFileStructure, CreateArgumentStructure>();
            services.AddSingleton<IBuildCommandFileStructure, CreateOptionsStructure>();
            services.AddSingleton<IBuildCommandFileStructure, CreateParameterClassStructure>();
            services.AddSingleton<IBuildCommandFileStructure, CreateSubCommandStructure>();
            services.AddSingleton<IBuildCommandFileStructure, CommandStructureBuilder>();
            services.AddSingleton<IBuildCommandFileStructure, CommandServiceStructureBuilder>();
        }

        private void RegisterTokenizer(IServiceCollection services)
        {
            services.AddSingleton<IExpressionTokenizer, Tokenizer.Tokenizer>();
            services.AddSingleton<ITokenizer, ArgumentTokenizer>();
            services.AddSingleton<ITokenizer, OptionTokenizer>();
            services.AddSingleton<ITokenizer, CommandTokenizer>();
        }

        private void RegisterValidation(IServiceCollection services)
        {
            services.AddSingleton<IProjectNameValidator, ProjectNameValidator>();
            services.AddSingleton<IToolNameValidator, ToolNameValidator>();
            services.AddSingleton<IOptionAliasValidator, OptionAliasValidator>();
            services.AddSingleton<IDescriptionValidator, DescriptionValidator>();

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
            services.AddSingleton<IExpressionContentValidator, DuplicatedCommandValidator>();
            services.AddSingleton<IExpressionContentValidator, RootCommandNameValidation>();
            services.AddSingleton<IExpressionContentValidator, ArgumentTypeValidator>();
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
            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<INameSpaceCollector, NameSpaceCollector>();
            services.AddSingleton<IRenameFilesAndFolders, RenameFilesAndFolders>();
            services.AddSingleton<ITemplateService, TemplateService>();
            services.AddSingleton<IDotNetToolTestService, DotNetToolTestService>();
            services.AddSingleton<IDotNetToolSerializer, DotNetToolSerializer>();
            services.AddSingleton<ITargetFolderService, TargetFolderService>();
            services.AddSingleton<ITypeService, TypeService>();

            services.AddSingleton<IUseIDE, UseIDE>();
            services.AddSingleton<ISpecificIDE, VisualStudio>();
            services.AddSingleton<ISpecificIDE, VisualStudioCode>();
            services.AddSingleton<ISpecificIDE, JetBrainsRider>();

            services.AddSingleton<ICollectTillInputCorrect, CollectTillInputCorrect>();
            services.AddSingleton<IBuiltInTypeTableService, BuiltInTypeTableService>();
        }

        private void RegisterDotNetToolInfoCollector(IServiceCollection services)
        {
            services.AddSingleton<ICollectDescription, CollectDescription>();
            services.AddSingleton<IDotNetToolInfoCollector, DotNetToolInfoCollector>();
            services.AddSingleton<ICollectProjectName, CollectProjectName>();
            services.AddSingleton<ICollectDotNetToolName, CollectDotNetToolName>();
            services.AddSingleton<ICollectOptionAlias, CollectOptionAlias>();
            services.AddSingleton<IParameterExpressionCollector, ParameterExpressionCollector>();
        }

        private void RegisterArgumentParser(IServiceCollection services)
        {
            services.AddSingleton<IArgumentParser, ArgumentParser>();
        }

        private void RegisterOptionsParser(IServiceCollection services)
        {
            services.AddSingleton<IOptionParser, OptionParser>();
        }

        private void RegisterParameterParser(IServiceCollection services)
        {
            services.AddSingleton<ICommandParser, CommandParser>();
            services.AddSingleton<IParameterExpressionParser, ParameterExpressionParser>();
        }

        private void RegisterArgumentBuilder(IServiceCollection services)
        {
            services.AddSingleton<IArgumentBuilder, ArgumentBuilder>();
            services.AddSingleton<IArgumentInterfaceBuilder, ArgumentInterfaceBuilder>();
        }

        private void RegisterOptionsBuilder(IServiceCollection services)
        {
            services.AddSingleton<IOptionInterfaceBuilder, OptionInterfaceBuilder>();
            services.AddSingleton<IOptionImplementationBuilder, OptionImplementationBuilder>();
            services.AddSingleton<IOptionMethodsBuilder, OptionMethodsBuilder>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithArgument>();
            services.AddSingleton<INewOptionExpressionBuilder, NewOptionExpressionBuilderWithoutArgument>();
            services.AddSingleton<INewOptionExpressionService, NewOptionExpressionService>();
        }

        private void RegisterParameterClassBuilder(IServiceCollection services)
        {
            services.AddSingleton<IParameterClassBuilder, ParameterClassBuilder>();
            services.AddSingleton<IParameterSpecificClassBuilder, ParameterWithoutArgsOrOptionsClassBuilder>();
            services.AddSingleton<IParameterSpecificClassBuilder, ParameterWithArgsOrOptionsClassBuilder>();
            services.AddSingleton<IConstructorArgumentBuilder, ConstructorArgumentBuilder>();
        }

        private void RegisterCommandBuilders(IServiceCollection services)
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
