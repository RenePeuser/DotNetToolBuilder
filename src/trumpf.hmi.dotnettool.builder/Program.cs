using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using trumpf.hmi.dotnettool.builder.Builder;
using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Services;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var projectName = new InsertProjectName().Invoke();
            Console.WriteLine();

            var dotNetToolName = new InsertDotNetToolName().Invoke();

            Console.WriteLine();
            var parameter = new InsertParameters().Collect();

            var directoryWithTemplate = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\Template");
            var newDirectory = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\New");

            newDirectory.Exists.IfTrueThen(() => newDirectory.Delete(true));

            // Copy template structure
            new CopyDirectory().DirectoryCopy(directoryWithTemplate.FullName, newDirectory.FullName);

            // Solution and projects
            new RenameFilesAndFolders().Rename(newDirectory, "Trumpf.Hmi.Uif", projectName);

            // DotNetTool name
            new RenameFilesAndFolders().Rename(newDirectory, "Uif", dotNetToolName.FirstCharToUpper());
            new RenameFilesAndFolders().Rename(newDirectory, "uif", dotNetToolName);

            // detect folder of root command
            var rootDirectory = newDirectory.EnumerateDirectories(dotNetToolName, SearchOption.AllDirectories).Single();

            var typeCollector = new CommandTypeCollector();
            var namespaceCollector = new NameSpaceCollector();

            var currentPath = projectName;

            // Create command structure
            new CreateCommandClasses().Invoke(projectName, parameter, rootDirectory, typeCollector, currentPath, namespaceCollector);

            // Find solution file
            var solutionFile = newDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            new StartUpBuilder().AddRegistrationsFrom(projectName, solutionFile, typeCollector, parameter, namespaceCollector);

            // fix name spaces
            // new NamspaceFixer().AddRegistrationsFrom(rootDirectory);

            // Now comes nice features :-)
            var processService = new ProcessService(new ProcessBuilder());


            Console.WriteLine();
            Console.WriteLine($"Build your new '{projectName}' dotnet tool...");
            Console.WriteLine();

            var dotnetBuildResult = await processService.RunCliCommandAsync("dotnet", $"build {solutionFile.FullName}");
            if (dotnetBuildResult.ExitCode != 0)
            {
                Console.WriteLine("Could not build sour new solution".AsError());

                // Open generated solution
                new VisualStudioService().Open(solutionFile);

                return;
            }

            Console.WriteLine(dotnetBuildResult.Output.AsSuccessfull());
            Console.WriteLine();

            var findExe = solutionFile.Directory.EnumerateFiles($"{projectName}.exe",SearchOption.AllDirectories).FirstOrDefault();


            Console.WriteLine($"Test run of your: '{projectName}' dotnet tool");
            Console.WriteLine();
            var runYourCliResult = await processService.RunCliCommandAsync($"{findExe.FullName}", "--help");

            Console.WriteLine();
            Console.WriteLine(runYourCliResult.Output.AsSuccessfull());
            Console.WriteLine();

            Console.WriteLine($"Enjoy your new generated: '{projectName}' dotnet tool :-)".AsSuccessfull());




            // Open generated solution
            new VisualStudioService().Open(solutionFile);
        }
    }
}
