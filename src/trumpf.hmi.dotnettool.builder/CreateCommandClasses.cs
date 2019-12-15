using System.Collections.Specialized;
using System.IO;
using System.Linq;
using trumpf.hmi.dotnettool.builder.Builder.Argument;
using trumpf.hmi.dotnettool.builder.Builder.Commands;
using trumpf.hmi.dotnettool.builder.Builder.Options;
using trumpf.hmi.dotnettool.builder.Builder.Parameter;
using Trumpf.Hmi.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    internal class CreateCommandClasses
    {
        public void Invoke(string projectName, CliParameterInfo parameter, DirectoryInfo rootDirectory,
            CommandTypeCollector commandTypeCollector, string currentPath, NameSpaceCollector namespaceCollector)
        {
            var subCommands = parameter.SubCommands;
            if (subCommands.IsNull())
            {
                return;
            }

            foreach (var subCommand in subCommands)
            {
                currentPath = $"{currentPath}.{subCommand.Name.FirstCharToUpper()}";

                namespaceCollector.Add(currentPath);

                var folderForCommand = Path.Combine(rootDirectory.FullName, subCommand.Name.FirstCharToUpper());
                var subCommnandDirectoryInfo = Directory.CreateDirectory(folderForCommand);

                if (subCommand.ArgumentInfo.IsNotNull())
                {
                    var argumentFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Arguments");
                    var argumentFolder = Directory.CreateDirectory(argumentFolderPath);

                    var argumentInterfaceSyntaxTree = new ArgumentInterfaceBuilder().Build(projectName, subCommand, currentPath);
                    var argumentInterfaceFilePath = new FileInfo(Path.Combine(argumentFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}ArgumentBuilder.cs"));
                    File.WriteAllText(argumentInterfaceFilePath.FullName, argumentInterfaceSyntaxTree);

                    var argumentImplementationSyntaxTree = new ArgumentBuilder().Build(projectName, subCommand, currentPath);
                    var argumentImplementationFilePath = new FileInfo(Path.Combine(argumentFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}ArgumentBuilder.cs"));
                    File.WriteAllText(argumentImplementationFilePath.FullName, argumentImplementationSyntaxTree);

                    commandTypeCollector.Add(parameter, new TypeToRegister(argumentInterfaceFilePath.FileNameWithoutExtension(), argumentImplementationFilePath.FileNameWithoutExtension()));
                }

                if (subCommand.Options.Any())
                {
                    var optionFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Options");
                    var optionFolder = Directory.CreateDirectory(optionFolderPath);

                    var optionsInterfaceSyntaxTree = new OptionInterfaceBuilder().Build(projectName, subCommand, currentPath);
                    var optionsInterfaceFilePath = new FileInfo(Path.Combine(optionFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}OptionsBuilder.cs"));
                    File.WriteAllText(optionsInterfaceFilePath.FullName, optionsInterfaceSyntaxTree);

                    var optionsImplementationSyntaxTree = new OptionImplementationBuilder().Build(projectName, subCommand, currentPath);
                    var optionsImplementationFilePath = new FileInfo(Path.Combine(optionFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}OptionsBuilder.cs"));
                    File.WriteAllText(optionsImplementationFilePath.FullName, optionsImplementationSyntaxTree);

                    commandTypeCollector.Add(parameter, new TypeToRegister(optionsInterfaceFilePath.FileNameWithoutExtension(), optionsImplementationFilePath.FileNameWithoutExtension()));
                }

                if (subCommand.Options.Any() || subCommand.ArgumentInfo.IsNotNull())
                {
                    var parameterModelClass = new ParameterClassBuilder().Build(projectName, subCommand, currentPath);
                    var filePath = Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}Parameters.cs");
                    File.WriteAllText(filePath, parameterModelClass);
                }


                if (subCommand.SubCommands.IsNotNull())
                {
                    if (subCommand.SubCommands.Any())
                    {
                        var result = new CommandBuilderForSubCommands().Build(projectName, subCommand, parameter, currentPath);
                        var filePath0 = new FileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}CommandBuilder.cs"));
                        File.WriteAllText(filePath0.FullName, result);
                        commandTypeCollector.Add(subCommand, new TypeToRegister($"I{parameter.Name.FirstCharToUpper()}CommandBuilder", filePath0.FileNameWithoutExtension()));

                        var subCommandBuilder = new SubCommandInterfaceBuilder().Build(projectName, subCommand, parameter, currentPath);
                        var filePath1 = new FileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"I{subCommand.Name.FirstCharToUpper()}SubCommandBuilder.cs"));
                        File.WriteAllText(filePath1.FullName, subCommandBuilder);
                    }
                }
                else
                {

                    string command = null;
                    if (subCommand.ArgumentInfo.IsNull() && subCommand.Options.IsEmpty())
                    {
                        command = new CommandBuilderSimple().Build(projectName, subCommand, parameter, currentPath);
                    }
                    else if(subCommand.ArgumentInfo.IsNotNull() && subCommand.Options.IsEmpty())
                    {
                        command = new CommandBuilderWithArgument().Build(projectName, subCommand, parameter, currentPath);
                    }
                    else if(subCommand.ArgumentInfo.IsNotNull() && subCommand.Options.Any())
                    {
                        command = new CommandBuilderWithArgumentAndOption().Build(projectName, subCommand, parameter, currentPath);
                    }

                    var filePath = new FileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}CommandBuilder.cs"));
                    File.WriteAllText(filePath.FullName, command);
                    commandTypeCollector.Add(subCommand, new TypeToRegister($"I{parameter.Name.FirstCharToUpper() }SubCommandBuilder", filePath.FileNameWithoutExtension()));


                    var commandServiceResult = new CommandServiceBuilder().Build(projectName, subCommand, currentPath);
                    var serviceFolder = new DirectoryInfo(Path.Combine(subCommnandDirectoryInfo.FullName, "Service"));
                    serviceFolder.Exists.IfFalseThen(() => serviceFolder.Create());
                    var commandService = new FileInfo(Path.Combine(serviceFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}Service.cs"));
                    File.WriteAllText(commandService.FullName, commandServiceResult);
                    
                    var serviceInterface = new CommandInterfaceBuilder().Build(projectName, subCommand, currentPath);
                    var commandServiceInterface = new FileInfo(Path.Combine(serviceFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}Service.cs"));
                    File.WriteAllText(commandServiceInterface.FullName, serviceInterface);

                    commandTypeCollector.Add(subCommand, new TypeToRegister($"{commandServiceInterface.FileNameWithoutExtension()}", $"{commandService.FileNameWithoutExtension()}"));
                }


                Invoke(projectName, subCommand, subCommnandDirectoryInfo, commandTypeCollector, currentPath, namespaceCollector);
            }
        }
    }
}