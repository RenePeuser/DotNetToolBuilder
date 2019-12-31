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
using Newtonsoft.Json;

namespace DotNetTool.Builder.App
{
    using Extensions;

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
            var consoleService = ServiceProvider.GetService<IConsoleService>();
            var dotNetToolInfoCollector = ServiceProvider.GetService<IDotNetToolInfoCollector>();
            var directoryService = ServiceProvider.GetService<IDirectoryService>();
            var fileService = ServiceProvider.GetService<IFileService>();
            var typeCollector = ServiceProvider.GetService<ICommandTypeCollector>();
            var namespaceCollector = ServiceProvider.GetService<INameSpaceCollector>();
            var visualStudioService = ServiceProvider.GetService<IVisualStudioService>();
            var renameFilesAndFolders = ServiceProvider.GetService<IRenameFilesAndFolders>();
            var createCommandClasses = ServiceProvider.GetService<ICreateCommandClasses>();
            var processService = ServiceProvider.GetService<IProcessService>();
            var startUpBuilder = ServiceProvider.GetService<IStartUpBuilder>();
            var extractTemplate = ServiceProvider.GetService<IExtractTemplate>();



            Models.DotNetTool dotNetTool = null;
            if (args.FirstOrDefault().IsNotNull() && args.First().EndsWith("json"))
            {
                var dotNetToolSerialized = fileService.GetFileInfo(args.FirstOrDefault());
                if (dotNetToolSerialized.Exists && dotNetToolSerialized.Extension.EndsWith("json"))
                {
                    dotNetTool = JsonConvert.DeserializeObject<Models.DotNetTool>(dotNetToolSerialized.ReadAllText());
                }
            }
            
            if(dotNetTool.IsNull())
            {
                dotNetTool = dotNetToolInfoCollector.Collect();
            }


            var targetDirectory = directoryService.GetDirectoryInfo(Path.Combine(directoryService.GetCurrentDirectory().FullName, dotNetTool.ProjectName));
            Throw.If(() => targetDirectory, dir => dir.Exists, $"The directory: {targetDirectory.FullName} already exists.");

            targetDirectory.Create();
            extractTemplate.ExtractTo(targetDirectory);

            // Solution and projects
            renameFilesAndFolders.Rename(targetDirectory, "rps.template", dotNetTool.ProjectName);

            // DotNetTool name
            renameFilesAndFolders.Rename(targetDirectory, "Rps", dotNetTool.NormalizedToolName);
            renameFilesAndFolders.Rename(targetDirectory, "rps", dotNetTool.ToolName.ToLower());

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
                consoleService.WriteError(dotnetBuildResult.Output);
                visualStudioService.Open(solutionFile);
                return -1;
            }

            consoleService.WriteSuccess(dotnetBuildResult.Output);
            var findExe = solutionFile.Directory.EnumerateFiles($"{dotNetTool.ProjectName}.exe", SearchOption.AllDirectories).FirstOrDefault();
            consoleService.WriteInfo($"Test run of your: '{dotNetTool.ProjectName}' dotnet tool");
            consoleService.WriteInfo($"{findExe.Name} --help");
            var runYourCliResult = await processService.RunCliCommandAsync($"{findExe.FullName}", "--help");
            consoleService.WriteSuccess(runYourCliResult.Output);
            consoleService.WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            visualStudioService.Open(solutionFile);

            return 0;
        }
    }
}
