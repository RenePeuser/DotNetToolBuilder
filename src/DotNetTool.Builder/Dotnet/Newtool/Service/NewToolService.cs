using System.Threading.Tasks;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Builders;
using DotNetTool.Builder.Services.DotNet;
using DotNetTool.Builder.Services.IDE;
using DotNetTool.Builder.SolutionBuilder.FileStructure;
using DotNetTool.Builder.SolutionBuilder.Services.IO;
using DotNetTool.Builder.SolutionBuilder.Services.Template;
using DotNetTool.Builder.SolutionBuilder.Startup;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using DotNetTool.Builder.ToolBuilder.FromConsole.Services;
using Extensions.Pack;

namespace DotNetTool.Builder.DotNet.Newtool.Service
{
    internal sealed class NewToolService(IDotNetToolSerializer dotNetToolSerializer,
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
                                         IConsoleService consoleService,
                                         DotNetToolToolBuildFromStrategy dotNetToolToolBuildFromStrategy,
                                         PackAsZipService packAsZipService) : INewToolService
    {
        public async Task<int> HandleAsync(NewToolParameters parameters)
        {
            // build dotnet tool.
            var dotNetTool = dotNetToolToolBuildFromStrategy.CreateFrom(parameters);

            // Save created tool as json.
            dotNetToolSerializer.Serialize(dotNetTool, parameters);

            // Create target, will create in execution folder and throws exception if target already exists.
            var targetDirectory = targetFolderService.CreateTargetDirectory(dotNetTool);

            // Extract the solution template to target directory
            templateExtractor.ExtractTo(targetDirectory);

            // All templates will renamed with the new tool information
            templateService.RenameAllIn(targetDirectory, dotNetTool);

            // detect folder of root command
            var rootDirectory = targetFolderService.GetToolFolder(dotNetTool, targetDirectory);

            // Create command structure
            createCommandClasses.Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, commandTypeCollector, dotNetTool.ProjectName, nameSpaceCollector);

            // Find solution file
            var solutionFile = targetFolderService.GetSolutionFile(dotNetTool, targetDirectory);

            // Add type registrations
            startUpBuilder.AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, commandTypeCollector, dotNetTool.ParameterInfo, nameSpaceCollector);

            // fast workaround to test it.
            if (parameters.UseFastMode.IsFalse())
            {
                // Build your new generated tool
                var dotnetBuildResult = await processService.RunAsync("dotnet", $"build {solutionFile.FullName}").ConfigureAwait(false);
                if (dotnetBuildResult.ExitCode != 0)
                {
                    // Opens all per option set IDE
                    await useIde.OpenAsync(solutionFile, parameters).ConfigureAwait(false);
                    return -1;
                }

                // Test run with the new tool with --help
                await dotNetToolTestService.RunAsync(solutionFile, dotNetTool).ConfigureAwait(false);
            }

            // Opens all per option set IDE
            await useIde.OpenAsync(solutionFile, parameters).ConfigureAwait(false);

            // new feature pack it as zip
            packAsZipService.PackAsync(targetDirectory, parameters);

            // All works fine, enjoy your new cli.
            consoleService.WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            return 0;
        }
    }
}
