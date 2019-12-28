using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Argument.Check;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.FileSystemAbstraction.Services;
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
            return RunInternalAsync();
        }

        private async Task<int> RunInternalAsync()
        {

            var consoleService = ServiceProvider.GetService<IConsoleService>();
            var dotNetToolInfoCollector = ServiceProvider.GetService<IDotNetToolInfoCollector>();
            var directoryService = ServiceProvider.GetService<IDirectoryService>();
            var typeCollector = ServiceProvider.GetService<ICommandTypeCollector>();
            var namespaceCollector = ServiceProvider.GetService<INameSpaceCollector>();
            var visualStudioService = ServiceProvider.GetService<IVisualStudioService>();
            var renameFilesAndFolders = ServiceProvider.GetService<IRenameFilesAndFolders>();
            var createCommandClasses = ServiceProvider.GetService<ICreateCommandClasses>();
            var processService = ServiceProvider.GetService<IProcessService>();
            var startUpBuilder = ServiceProvider.GetService<IStartUpBuilder>();
            var extractTemplate = ServiceProvider.GetService<IExtractTemplate>();


            var dotNetTool = dotNetToolInfoCollector.Collect();

            var targetDirectory = directoryService.GetDirectoryInfo(Path.Combine(directoryService.GetCurrentDirectory().FullName, dotNetTool.ProjectName));
            Throw.If(() => targetDirectory, dir => dir.Exists, $"The directory: {targetDirectory.FullName} already exists.");

            targetDirectory.Create();
            extractTemplate.ExtractTo(targetDirectory);

            // Solution and projects
            renameFilesAndFolders.Rename(targetDirectory, "rps.template", dotNetTool.ProjectName);

            // DotNetTool name
            renameFilesAndFolders.Rename(targetDirectory, "Rps", dotNetTool.NormalizedToolName);
            renameFilesAndFolders.Rename(targetDirectory, "rps", dotNetTool.ToolName);

            // detect folder of root command
            var rootDirectory = targetDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();
            var currentPath = dotNetTool.ProjectName;

            // Create command structure
            createCommandClasses.Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, typeCollector, currentPath, namespaceCollector);

            // Find solution file
            var solutionFile = targetDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            startUpBuilder.AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, typeCollector, dotNetTool.ParameterInfo, namespaceCollector);

            consoleService.WriteInfo($"Build your new '{dotNetTool.ProjectName}' dotnet tool...");

            var dotnetBuildResult = await processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                consoleService.WriteError("Could not build sour new solution");
                visualStudioService.Open(solutionFile);
                return -1;
            }

            consoleService.WriteSuccess(dotnetBuildResult.Output);

            var findExe = solutionFile.Directory.EnumerateFiles($"{dotNetTool.ProjectName}.exe", SearchOption.AllDirectories).FirstOrDefault();
            consoleService.WriteInfo($"Test run of your: '{dotNetTool.ProjectName}' dotnet tool");

            var runYourCliResult = await processService.RunCliCommandAsync($"{findExe.FullName}", "--help");
            consoleService.WriteSuccess(runYourCliResult.Output);
            consoleService.WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            visualStudioService.Open(solutionFile);

            return 0;
        }
    }
}
