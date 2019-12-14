using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Pastel;

namespace trumpf.hmi.dotnettool.builder
{
    public static class ColorExtension
    {
        public static string InputColor(this string source)
        {
            return source.Pastel(Color.Gray);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var projectName = new InsertProjectName().Invoke();
            Console.WriteLine();

            var dotNetToolName = new InsertDotNetToolName().Invoke();

            Console.WriteLine();
            var parameter = new InsertParameters().Collect();

            var directoryWithTemplate = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\Template");
            var newDirectory = new DirectoryInfo(@"D:\AzureDevOps\DotNetToolBuilder\src\New");

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

            // Create command structure
            new CreateCommandClasses().Invoke(projectName, parameter, rootDirectory, typeCollector);

            // Find solution file
            var solutionFile = newDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Add type registrations
            new StartUpBuilder().AddRegistrationsFrom(projectName, solutionFile, typeCollector, parameter);

            // Open generated solution
            new VisualStudioService().Open(solutionFile);
        }
    }
}
