using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Builder;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Services;
using Microsoft.Extensions.DependencyInjection;
using Trumpf.Hmi.Extensions;
using Trumpf.Hmi.FileSystemAbstraction.Services;

namespace DotNetTool.Builder.App
{
    public class App
    {
        public IServiceProvider ServiceProvider { get; }

        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Task<int> RunAsync(string[] args)
        {
            return RunInternalAsync();
        }

        private async Task<int> RunInternalAsync()
        {
            var consoleService = ServiceProvider.GetService<IConsoleService>();
            var dotNetToolCollector = ServiceProvider.GetService<IDotNetToolInfoCollector>();
            var directoryService = ServiceProvider.GetService<TiDirectoryService>();
            var typeCollector = ServiceProvider.GetService<ICommandTypeCollector>();
            var namespaceCollector = ServiceProvider.GetService<INameSpaceCollector>();
            var visualStudioService = ServiceProvider.GetService<IVisualStudioService>();
            var copyDirectoryService = ServiceProvider.GetService<ICopyDirectoryService>();
            var renameFilesAndFolders = ServiceProvider.GetService<IRenameFilesAndFolders>();
            var createCommandClasses = ServiceProvider.GetService<ICreateCommandClasses>();
            var processService = ServiceProvider.GetService<IProcessService>();
            var startUpBuilder = ServiceProvider.GetService<IStartUpBuilder>();


            var dotNetTool = dotNetToolCollector.Collect();

            var sourceDirectory = directoryService.GetDirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\Template");
            var targetDirectory = directoryService.GetDirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\New");

            targetDirectory.Exists.IfTrueThen(() => targetDirectory.Delete(true));

            // Copy template structure
            copyDirectoryService.CopyDirectory(sourceDirectory, targetDirectory);

            // Solution and projects
            renameFilesAndFolders.Rename(targetDirectory, "Trumpf.Hmi.Uif", dotNetTool.ProjectName);

            // DotNetTool name
            renameFilesAndFolders.Rename(targetDirectory, "Uif", dotNetTool.ToolName.FirstCharToUpper());
            renameFilesAndFolders.Rename(targetDirectory, "uif", dotNetTool.ToolName);

            // detect folder of root command
            var rootDirectory = targetDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();

            var currentPath = dotNetTool.ProjectName;

            // Create command structure
            createCommandClasses.Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, typeCollector, currentPath, namespaceCollector);

            // Find solution file
            var solutionFile = targetDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            startUpBuilder.AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, typeCollector, dotNetTool.ParameterInfo, namespaceCollector);

            consoleService.WriteLine();
            consoleService.WriteLine($"Build your new '{dotNetTool.ProjectName}' dotnet tool...");
            consoleService.WriteLine();

            var dotnetBuildResult = await processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                consoleService.WriteLine("Could not build sour new solution".AsError());

                // Open generated solution
                visualStudioService.Open(solutionFile);

                return -1;
            }

            consoleService.WriteLine(dotnetBuildResult.Output.AsSuccessfull());
            consoleService.WriteLine();

            var findExe = solutionFile.Directory.EnumerateFiles($"{dotNetTool.ProjectName}.exe", SearchOption.AllDirectories).FirstOrDefault();


            consoleService.WriteLine($"Test run of your: '{dotNetTool.ProjectName}' dotnet tool");
            consoleService.WriteLine();
            var runYourCliResult = await processService.RunCliCommandAsync($"{findExe.FullName}", "--help");

            consoleService.WriteLine();
            consoleService.WriteLine(runYourCliResult.Output.AsSuccessfull());
            consoleService.WriteLine();

            consoleService.WriteLine($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)".AsSuccessfull());

            // Open generated solution
            visualStudioService.Open(solutionFile);

            return 0;
        }
    }
}