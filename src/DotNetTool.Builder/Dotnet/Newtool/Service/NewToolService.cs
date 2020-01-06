using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Builder.FileStructure;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Collectors;
using DotNetTool.Builder.Services.DotNet;
using DotNetTool.Builder.Services.IDE;
using DotNetTool.Builder.Services.IO;
using DotNetTool.Builder.Services.Process;
using DotNetTool.Builder.Services.Template;

namespace DotNetTool.Builder.Dotnet.Newtool.Service
{
    internal class NewToolService : INewToolService
    {
        private readonly ICommandTypeCollector _commandTypeCollector;
        private readonly IConsoleService _consoleService;
        private readonly ICreateCommandClasses _createCommandClasses;
        private readonly IDotNetToolInfoCollector _dotNetToolInfoCollector;
        private readonly IDotNetToolSerializer _dotNetToolSerializer;
        private readonly IDotNetToolTestService _dotNetToolTestService;
        private readonly INameSpaceCollector _nameSpaceCollector;
        private readonly IProcessService _processService;
        private readonly IStartUpBuilder _startUpBuilder;
        private readonly ITargetFolderService _targetFolderService;
        private readonly ITemplateExtractor _templateExtractor;
        private readonly ITemplateService _templateService;
        private readonly IUseIDE _useIde;

        public NewToolService(
            IDotNetToolSerializer dotNetToolSerializer,
            IDotNetToolInfoCollector dotNetToolInfoCollector,
            ITargetFolderService targetFolderService,
            ITemplateExtractor templateExtractor,
            ITemplateService templateService,
            ICreateCommandClasses createCommandClasses,
            ICommandTypeCollector commandTypeCollector,
            INameSpaceCollector nameSpaceCollector,
            IStartUpBuilder startUpBuilder,
            IProcessService processService,
            IUseIDE useIde,
            IDotNetToolTestService dotNetToolTestService,
            IConsoleService consoleService)
        {
            _dotNetToolSerializer = dotNetToolSerializer;
            _dotNetToolInfoCollector = dotNetToolInfoCollector;
            _targetFolderService = targetFolderService;
            _templateExtractor = templateExtractor;
            _templateService = templateService;
            _createCommandClasses = createCommandClasses;
            _commandTypeCollector = commandTypeCollector;
            _nameSpaceCollector = nameSpaceCollector;
            _startUpBuilder = startUpBuilder;
            _processService = processService;
            _useIde = useIde;
            _dotNetToolTestService = dotNetToolTestService;
            _consoleService = consoleService;
        }

        public async Task<int> HandleAsync(NewToolParameters parameters)
        {
            // if a json file with a dot net tool is given then try to deserialize it
            var dotNetTool = _dotNetToolSerializer.DeserializeFrom(parameters.FromFile);
            if (dotNetTool.IsNull())
            {
                // if tool was not deserialized, then user have to give in all information for this tool.
                dotNetTool = _dotNetToolInfoCollector.Collect();
            }

            // Save created tool as json.
            _dotNetToolSerializer.Serialize(dotNetTool, parameters);

            // Create target, will create in execution folder and throws exception if target already exists.
            var targetDirectory = _targetFolderService.CreateTargetDirectory(dotNetTool);

            // Extract the solution template to target directory
            _templateExtractor.ExtractTo(targetDirectory);

            // All templates will renamed with the new tool information
            _templateService.RenameAllIn(targetDirectory, dotNetTool);

            // detect folder of root command
            var rootDirectory = targetDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();

            // Create command structure
            _createCommandClasses.Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, _commandTypeCollector, dotNetTool.ProjectName, _nameSpaceCollector);

            // Find solution file
            var solutionFile = targetDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            _startUpBuilder.AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, _commandTypeCollector, dotNetTool.ParameterInfo, _nameSpaceCollector);

            // Build your new generated tool
            var dotnetBuildResult = await _processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}").ConfigureAwait(false);
            if (dotnetBuildResult.ExitCode != 0)
            {
                // Opens all per option set IDE
                await _useIde.OpenAsync(solutionFile, parameters).ConfigureAwait(false);
                return -1;
            }

            // Test run with the new tool with --help
            await _dotNetToolTestService.RunAsync(solutionFile, dotNetTool).ConfigureAwait(false);

            // Opens all per option set IDE
            await _useIde.OpenAsync(solutionFile, parameters).ConfigureAwait(false);

            // All works fine, enjoy your new cli.
            _consoleService.WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            return 0;
        }
    }
}
