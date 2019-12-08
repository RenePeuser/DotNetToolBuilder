using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Pastel;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class RenameFilesAndFolders
    {
        public void Rename(DirectoryInfo directoryInfo, string originalName, string newName)
        {
            var folders = directoryInfo.EnumerateDirectories("*.*", SearchOption.AllDirectories);
            foreach (var folder in folders)
            {
                if (folder.Name.Contains(originalName))
                {
                    Directory.Move(folder.FullName, folder.FullName.Replace(originalName, newName));
                }
            }

            var allFiles = directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories);
            foreach (var fileInfo in allFiles)
            {
                var content = File.ReadAllText(fileInfo.FullName);
                if (content.Contains(originalName))
                {
                    var newContent = content.Replace(originalName, newName);
                    File.WriteAllText(fileInfo.FullName, newContent);
                }

                if (fileInfo.Name.Contains(originalName))
                {
                    File.Move(fileInfo.FullName, fileInfo.FullName.Replace(originalName, newName));
                }
            }
        }
    }

    public class CopyDirectory
    {
        public void DirectoryCopy(string sourceDirName, string destDirName)
        {
            DirectoryCopy(sourceDirName, destDirName, true);
        }

        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }

    public abstract class CollectInfoStep : ICollectInfo
    {
        protected CollectInfoStep(string title)
        {
            Title = title;
        }

        public string Title { get; }

        public virtual string Invoke()
        {
            Console.WriteLine(Title);
            return Console.ReadLine();
        }
    }

    public interface ICollectInfo
    {
        string Title { get; }
    }

    public class InsertProjectName : CollectInfoStep
    {
        public InsertProjectName() : base("Please enter the name of your project: (Sample: Trumpf.Hmi.New.Submarine)".Pastel(Color.Yellow))
        {
        }
    }

    public class InsertDotNetToolName : CollectInfoStep
    {
        private static readonly string title = "Please enter the name of the DotNetTool: Sample: 'dotnet'".Pastel(Color.Yellow) +
                                               $"{Environment.NewLine}" +
                                               $"Usage: 'dotnet tool list packages --global'".Pastel(Color.GreenYellow);

        public InsertDotNetToolName() : base(title)
        {
        }
    }

    public class InsertParameters
    {
        public CliParameterInfo Collect()
        {
            CliParameterInfo parameter = null;

            bool noParametersRequired = false;
            while (noParametersRequired.IsFalse())
            {
                Console.WriteLine($"Do you want to add a parameter expression ? yes(y) or no (n)".Pastel(Color.Yellow));

                var result = Console.ReadLine();
                if (result.Contains("no") || result.Contains("n"))
                {
                    return parameter;
                }

                Console.WriteLine();
                Console.WriteLine("Please enter your parameter expression: (Sample: dotnet tool list packages --global)".Pastel(Color.Yellow) + $"{Environment.NewLine}Please write your options in long terms with '--myOption')".Pastel(Color.GreenYellow));
                var parameterExpression = Console.ReadLine();

                var parseResult = new ParameterExpressionParser().Parse(parameterExpression, parameter);
                if (parameter.IsNull())
                {
                    parameter = parseResult;
                }

                Console.WriteLine();
            }

            return parameter;
        }
    }

    public class ParameterExpressionParser
    {
        public CliParameterInfo Parse(string paramterExpression, CliParameterInfo lastParamater)
        {
            var splittedExpression = paramterExpression.Split(" ");

            var listArguments = new List<OptionInfo>();
            CliParameterInfo lastCliParameterInfo = null;

            for (int i = splittedExpression.Length - 1; i >= 0; i--)
            {
                var current = splittedExpression[i];
                if (current.Contains("-"))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Please enter an alias for your option: '{current}'".Pastel(Color.Yellow));
                    var alias = Console.ReadLine();
                    Console.WriteLine();

                    Console.WriteLine($"Please enter a description for your option: '{current}'".Pastel(Color.Yellow));
                    var description = Console.ReadLine();

                    listArguments.Add(new OptionInfo(current, alias, description));
                }
                else
                {
                    var parameter = new CliParameterInfo();
                    parameter.Name = current;

                    Console.WriteLine();
                    Console.WriteLine($"Please enter a description for your command: '{parameter.Name}'".Pastel(Color.Yellow));
                    var description = Console.ReadLine();
                    parameter.Decsription = description;
                    parameter.Options = listArguments.ToList();
                    listArguments = new List<OptionInfo>();
                    if (lastCliParameterInfo.IsNotNull())
                    {
                        parameter.SubCommands = lastCliParameterInfo.ToIList();
                    }

                    lastCliParameterInfo = parameter;

                    if (lastParamater.IsNotNull())
                    {
                        var parentForThis = CliParameterService.FindAlreadyExistingCommand(lastParamater.SubCommands, parameter);
                        if (parentForThis.IsNotNull())
                        {
                            parentForThis.SubCommands = parentForThis.SubCommands.Concat(parameter.SubCommands);
                        }
                    }
                }
            }

            if (lastParamater.IsNotNull())
            {
                return lastParamater;
            }

            return lastCliParameterInfo;
        }
    }

    public class CliParameterInfo
    {
        public IEnumerable<CliParameterInfo> SubCommands { get; set; }

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public string Name { get; set; }

        public string Decsription { get; set; }
    }

    public class OptionInfo
    {
        public OptionInfo(string name, string alias, string description)
        {
            Name = name;
            Alias = alias;
            Description = description;
        }

        public string Name { get; }
        public string Alias { get; }
        public string Description { get; }
    }

    public class VisualStudioService
    {
        public void Open(FileInfo solution)
        {
            var programx86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var vs2019 = Path.Combine(programx86Path, @"Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe");
            var fileInfo = new FileInfo(vs2019);
            Process.Start(fileInfo.FullName, solution.FullName);
        }
    }

    public class CliParameterService
    {
        public static CliParameterInfo FindAlreadyExistingCommand(IEnumerable<CliParameterInfo> others, CliParameterInfo current)
        {
            if (others.IsNull())
            {
                return null;
            }

            foreach (var cliParameterInfo in others)
            {
                if (cliParameterInfo.Name == current.Name)
                {
                    return cliParameterInfo;
                }

                var match = FindAlreadyExistingCommand(cliParameterInfo.SubCommands, current);
                if (match.IsNotNull())
                {
                    return match;
                }
            }

            return null;
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

            var directoryWithTemplate = new DirectoryInfo(@"D:\Gitlab\trumpf.hmi.dotnettool.builder\src\Template");
            var newDirectory = new DirectoryInfo(@"D:\Gitlab\trumpf.hmi.dotnettool.builder\src\New\");

            // Copy template structure
            new CopyDirectory().DirectoryCopy(directoryWithTemplate.FullName, newDirectory.FullName);

            // Solution and projects
            new RenameFilesAndFolders().Rename(newDirectory, "Trumpf.Hmi.Uif", projectName);

            // DotNetTool name
            new RenameFilesAndFolders().Rename(newDirectory, "Uif", dotNetToolName);
            new RenameFilesAndFolders().Rename(newDirectory, "uif", dotNetToolName);

            // detect folder of root command
            var rootDirectory = newDirectory.EnumerateDirectories(dotNetToolName, SearchOption.AllDirectories).Single();

            // Create command structure
            new CreateCommandClasses().Invoke(parameter, rootDirectory);


            // Find solution file
            var solutionFile = newDirectory.EnumerateFiles("*.sln", SearchOption.AllDirectories).Single();

            // Open generated solution
            new VisualStudioService().Open(solutionFile);
        }
    }

    internal class CreateCommandClasses
    {
        public void Invoke(CliParameterInfo parameter, DirectoryInfo rootDirectory)
        {
            var subCommands = parameter.SubCommands;
            if (subCommands.IsNull())
            {
                return;
            }

            foreach (var subCommand in subCommands)
            {
                var folderForCommand = Path.Combine(rootDirectory.FullName, subCommand.Name);
                var subCommnandDirectoryInfo = Directory.CreateDirectory(folderForCommand);
                Invoke(subCommand, subCommnandDirectoryInfo);
            }
        }
    }
}
