using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetTool.Builder.App
{
    public class App
    {
        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        public Task<int> RunAsync(string[] args)
        {
            return RunInternalAsync(args);
        }

        private async Task<int> RunInternalAsync(string[] args)
        {
            var dotNetToolInfoCollector = ServiceProvider.GetService<IDotNetToolInfoCollector>();
            var typeCollector = ServiceProvider.GetService<ICommandTypeCollector>();
            var namespaceCollector = ServiceProvider.GetService<INameSpaceCollector>();
            var visualStudioService = ServiceProvider.GetService<IVisualStudioService>();
            var templateService = ServiceProvider.GetService<ITemplateService>();
            var createCommandClasses = ServiceProvider.GetService<ICreateCommandClasses>();
            var processService = ServiceProvider.GetService<IProcessService>();
            var startUpBuilder = ServiceProvider.GetService<IStartUpBuilder>();
            var templateExtractor = ServiceProvider.GetService<ITemplateExtractor>();
            var dotNetToolService = ServiceProvider.GetService<IDotNetToolTestService>();
            var dotNetToolSerializer = ServiceProvider.GetService<IDotNetToolSerializer>();
            var targetFolderService = ServiceProvider.GetService<ITargetFolderService>();
            var consoleService = ServiceProvider.GetService<IConsoleService>();

            // if a json file with a dot net tool is given then try to deserialize it
            var dotNetTool = dotNetToolSerializer.DeserializeFrom(args.FirstOrDefault());
            if (dotNetTool.IsNull())
            {
                // if tool was not deserialized, then user have to give in all information for this tool.
                dotNetTool = dotNetToolInfoCollector.Collect();
            }

            // Create target, will create in execution folder and throws exception if target already exists.
            var targetDirectory = targetFolderService.CreateTargetDirectory(dotNetTool);

            // Extract the solution template to target directory
            templateExtractor.ExtractTo(targetDirectory);

            // All templates will renamed with the new tool information
            templateService.RenameAllIn(targetDirectory, dotNetTool);

            // detect folder of root command
            var rootDirectory = targetDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();

            // Create command structure
            createCommandClasses.Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, typeCollector, dotNetTool.ProjectName, namespaceCollector);

            // Find solution file
            var solutionFile = targetDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            startUpBuilder.AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, typeCollector, dotNetTool.ParameterInfo, namespaceCollector);

            // Build your new generated tool
            var dotnetBuildResult = await processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                // Also if fail open visual studio, to focus to the error, most case will be incorrect type casts for arguments.
                await visualStudioService.OpenAsync(solutionFile);
                return -1;
            }

            // Test run with the new tool with --help
            await dotNetToolService.RunAsync(solutionFile, dotNetTool);

            // Open visual studio, right now works only with VS2019 !
            await visualStudioService.OpenAsync(solutionFile);

            // All works fine, enjoy your new cli.
            consoleService.WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            return 0;
        }
    }
}
