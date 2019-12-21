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

            var dotNetTool = dotNetToolCollector.Collect();

            var directoryWithTemplate = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\Template");
            var newDirectory = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\New");

            newDirectory.Exists.IfTrueThen(() => newDirectory.Delete(true));

            // Copy template structure
            new CopyDirectory().DirectoryCopy(directoryWithTemplate.FullName, newDirectory.FullName);

            // Solution and projects
            new RenameFilesAndFolders().Rename(newDirectory, "Trumpf.Hmi.Uif", dotNetTool.ProjectName);

            // DotNetTool name
            new RenameFilesAndFolders().Rename(newDirectory, "Uif", dotNetTool.ToolName.FirstCharToUpper());
            new RenameFilesAndFolders().Rename(newDirectory, "uif", dotNetTool.ToolName);

            // detect folder of root command
            var rootDirectory = newDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();

            var typeCollector = new CommandTypeCollector();
            var namespaceCollector = new NameSpaceCollector();

            var currentPath = dotNetTool.ProjectName;

            // Create command structure
            new CreateCommandClasses().Invoke(dotNetTool.ProjectName, dotNetTool.CliParameterInfo, rootDirectory, typeCollector, currentPath, namespaceCollector);

            // Find solution file
            var solutionFile = newDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            new StartUpBuilder().AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, typeCollector, dotNetTool.CliParameterInfo, namespaceCollector);

            // fix name spaces
            // new NamspaceFixer().AddRegistrationsFrom(rootDirectory);

            // Now comes nice features :-)
            var processService = new ProcessService(new ProcessBuilder());


            consoleService.WriteLine();
            consoleService.WriteLine($"Build your new '{dotNetTool.ProjectName}' dotnet tool...");
            consoleService.WriteLine();

            var dotnetBuildResult = await processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                consoleService.WriteLine("Could not build sour new solution".AsError());

                // Open generated solution
                new VisualStudioService().Open(solutionFile);

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
            new VisualStudioService().Open(solutionFile);

            return 0;
        }
    }
}