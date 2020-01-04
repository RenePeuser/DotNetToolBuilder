using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Builder.Startup;
using DotNetTool.Builder.FileStructure;
using DotNetTool.Builder.InfoCollectors;
using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.App
{
    internal class App : ServiceProviderBase
    {
        public App(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        internal Task<int> RunAsync(string[] args)
        {
            return RunInternalAsync(args);
        }

        private async Task<int> RunInternalAsync(string[] args)
        {
            // if a json file with a dot net tool is given then try to deserialize it
            var dotNetTool = Use<IDotNetToolSerializer>().DeserializeFrom(args.FirstOrDefault());
            if (dotNetTool.IsNull())
            {
                // if tool was not deserialized, then user have to give in all information for this tool.
                dotNetTool = Use<IDotNetToolInfoCollector>().Collect();
            }

            // Create target, will create in execution folder and throws exception if target already exists.
            var targetDirectory = Use<ITargetFolderService>().CreateTargetDirectory(dotNetTool);

            // Extract the solution template to target directory
            Use<ITemplateExtractor>().ExtractTo(targetDirectory);

            // All templates will renamed with the new tool information
            Use<ITemplateService>().RenameAllIn(targetDirectory, dotNetTool);

            // detect folder of root command
            var rootDirectory = targetDirectory.EnumerateDirectories(dotNetTool.ToolName, SearchOption.AllDirectories).Single();

            // Create command structure
            Use<ICreateCommandClasses>().Invoke(dotNetTool.ProjectName, dotNetTool.ParameterInfo, rootDirectory, Get<ICommandTypeCollector>(), dotNetTool.ProjectName, Get<INameSpaceCollector>());

            // Find solution file
            var solutionFile = targetDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            Use<IStartUpBuilder>().AddRegistrationsFrom(dotNetTool.ProjectName, solutionFile, Get<ICommandTypeCollector>(), dotNetTool.ParameterInfo, Get<INameSpaceCollector>());

            // Build your new generated tool
            var dotnetBuildResult = await Use<IProcessService>().RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                // Also if fail open visual studio, to focus to the error, most case will be incorrect type casts for arguments.
                await Use<IVisualStudioService>().OpenAsync(solutionFile);
                return -1;
            }

            // Test run with the new tool with --help
            await Use<IDotNetToolTestService>().RunAsync(solutionFile, dotNetTool);

            // Open visual studio, right now works only with VS2019 !
            await Use<IVisualStudioService>().OpenAsync(solutionFile);

            // All works fine, enjoy your new cli.
            Use<IConsoleService>().WriteSuccess($"Enjoy your new generated: '{dotNetTool.ProjectName}' dotnet tool :-)");

            return 0;
        }
    }
}
