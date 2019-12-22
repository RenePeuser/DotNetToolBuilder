using System.IO;
using System.Linq;
using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;
using Trumpf.Hmi.FileSystemAbstraction.FileSystem;
using Trumpf.Hmi.FileSystemAbstraction.Services;

namespace DotNetTool.Builder.Services
{
    internal class CreateCommandClasses : ICreateCommandClasses
    {
        private readonly IArgumentInterfaceBuilder _argumentInterfaceBuilder;
        private readonly IOptionInterfaceBuilder _optionInterfaceBuilder;
        private readonly ICommandBuilderForSubCommands _commandBuilderForSubCommands;
        private readonly ISubCommandInterfaceBuilder _subCommandInterfaceBuilder;
        private readonly ICommandBuilderSimple _commandBuilderSimple;
        private readonly ICommandBuilderWithArgument _commandBuilderWithArgument;
        private readonly ICommandBuilderWithArgumentAndOption _commandBuilderWithArgumentAndOption;
        private readonly TiDirectoryService _directoryService;
        private readonly TiFileService _fileService;
        private readonly IArgumentBuilder _argumentBuilder;
        private readonly IOptionImplementationBuilder _optionImplementationBuilder;
        private readonly IParameterClassBuilder _parameterClassBuilder;
        private readonly ICommandServiceBuilder _commandServiceBuilder;
        private readonly ICommandInterfaceBuilder _commandInterfaceBuilder;

        public CreateCommandClasses(IArgumentInterfaceBuilder argumentInterfaceBuilder,
                                    IOptionInterfaceBuilder optionInterfaceBuilder, 
                                    ICommandBuilderForSubCommands commandBuilderForSubCommands, 
                                    ISubCommandInterfaceBuilder subCommandInterfaceBuilder,
                                    ICommandBuilderSimple commandBuilderSimple,
                                    ICommandBuilderWithArgument commandBuilderWithArgument,
                                    ICommandBuilderWithArgumentAndOption commandBuilderWithArgumentAndOption,
                                    TiDirectoryService directoryService,
                                    TiFileService fileService,
                                    IArgumentBuilder argumentBuilder,
                                    IOptionImplementationBuilder optionImplementationBuilder,
                                    IParameterClassBuilder parameterClassBuilder,
                                    ICommandServiceBuilder commandServiceBuilder,
                                    ICommandInterfaceBuilder commandInterfaceBuilder)
        {
            _argumentInterfaceBuilder = argumentInterfaceBuilder;
            _optionInterfaceBuilder = optionInterfaceBuilder;
            _commandBuilderForSubCommands = commandBuilderForSubCommands;
            _subCommandInterfaceBuilder = subCommandInterfaceBuilder;
            _commandBuilderSimple = commandBuilderSimple;
            _commandBuilderWithArgument = commandBuilderWithArgument;
            _commandBuilderWithArgumentAndOption = commandBuilderWithArgumentAndOption;
            _directoryService = directoryService;
            _fileService = fileService;
            _argumentBuilder = argumentBuilder;
            _optionImplementationBuilder = optionImplementationBuilder;
            _parameterClassBuilder = parameterClassBuilder;
            _commandServiceBuilder = commandServiceBuilder;
            _commandInterfaceBuilder = commandInterfaceBuilder;
        }

        public void Invoke(string projectName, ParameterInfo parameter, TiDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector)
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

                var subCommnandDirectoryInfo = _directoryService.GetDirectoryInfo(Path.Combine(rootDirectory.FullName, subCommand.Name.FirstCharToUpper()));
                subCommnandDirectoryInfo.Create();

                if (subCommand.ArgumentInfo.IsNotNull())
                {
                    var argumentFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Arguments");
                    var argumentFolder = _directoryService.CreateDirectory(argumentFolderPath);

                    var argumentInterfaceSyntaxTree = _argumentInterfaceBuilder.Build(projectName, subCommand, currentPath);
                    var argumentInterfaceFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}ArgumentBuilder.cs"));
                    File.WriteAllText(argumentInterfaceFilePath.FullName, argumentInterfaceSyntaxTree);

                    var argumentImplementationSyntaxTree = _argumentBuilder.Build(projectName, subCommand, currentPath);
                    var argumentImplementationFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}ArgumentBuilder.cs"));
                    File.WriteAllText(argumentImplementationFilePath.FullName, argumentImplementationSyntaxTree);

                    commandTypeCollector.Add(parameter, new TypeToRegister(argumentInterfaceFilePath.FileNameWithoutExtension(), argumentImplementationFilePath.FileNameWithoutExtension()));
                }

                if (subCommand.Options.Any())
                {
                    var optionFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Options");
                    var optionFolder = _directoryService.CreateDirectory(optionFolderPath);

                    var optionsInterfaceSyntaxTree = _optionInterfaceBuilder.Build(projectName, subCommand, currentPath);
                    var optionsInterfaceFilePath = _fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}OptionsBuilder.cs"));
                    File.WriteAllText(optionsInterfaceFilePath.FullName, optionsInterfaceSyntaxTree);

                    var optionsImplementationSyntaxTree = _optionImplementationBuilder.Build(projectName, subCommand, currentPath);
                    var optionsImplementationFilePath = _fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}OptionsBuilder.cs"));
                    File.WriteAllText(optionsImplementationFilePath.FullName, optionsImplementationSyntaxTree);

                    commandTypeCollector.Add(parameter, new TypeToRegister(optionsInterfaceFilePath.FileNameWithoutExtension(), optionsImplementationFilePath.FileNameWithoutExtension()));
                }

                if (subCommand.Options.Any() || subCommand.ArgumentInfo.IsNotNull())
                {
                    var parameterModelClass = _parameterClassBuilder.Build(projectName, subCommand, currentPath);
                    var fileInfo = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}Parameters.cs"));
                    fileInfo.WriteAllText(parameterModelClass);
                }


                if (subCommand.SubCommands.IsNotNull())
                {
                    if (subCommand.SubCommands.Any())
                    {
                        var result = _commandBuilderForSubCommands.Build(projectName, subCommand, parameter, currentPath);
                        var filePath0 = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}CommandBuilder.cs"));
                        filePath0.WriteAllText(result);
                        commandTypeCollector.Add(subCommand, new TypeToRegister($"I{parameter.Name.FirstCharToUpper()}SubCommandBuilder", filePath0.FileNameWithoutExtension()));

                        var subCommandBuilder = _subCommandInterfaceBuilder.Build(projectName, subCommand, parameter, currentPath);
                        var filePath1 = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"I{subCommand.Name.FirstCharToUpper()}SubCommandBuilder.cs"));
                        filePath1.WriteAllText(subCommandBuilder);
                    }
                }
                else
                {

                    string command = null;
                    if (subCommand.ArgumentInfo.IsNull() && subCommand.Options.IsEmpty())
                    {
                        command = _commandBuilderSimple.Build(projectName, subCommand, parameter, currentPath);
                    }
                    else if (subCommand.ArgumentInfo.IsNotNull() && subCommand.Options.IsEmpty())
                    {
                        command = _commandBuilderWithArgument.Build(projectName, subCommand, parameter, currentPath);
                    }
                    else if (subCommand.ArgumentInfo.IsNotNull() && subCommand.Options.Any())
                    {
                        command = _commandBuilderWithArgumentAndOption.Build(projectName, subCommand, parameter, currentPath);
                    }

                    var fileInfo = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.Name.FirstCharToUpper()}CommandBuilder.cs"));
                    fileInfo.WriteAllText(command);
                    commandTypeCollector.Add(subCommand, new TypeToRegister($"I{parameter.Name.FirstCharToUpper() }SubCommandBuilder", fileInfo.FileNameWithoutExtension()));


                    var commandServiceResult = _commandServiceBuilder.Build(projectName, subCommand, currentPath);
                    var serviceFolder = _directoryService.GetDirectoryInfo(Path.Combine(subCommnandDirectoryInfo.FullName, "Service"));
                    serviceFolder.Exists.IfFalseThen(() => serviceFolder.Create());
                    var commandService = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"{subCommand.Name.FirstCharToUpper()}Service.cs"));
                    commandService.WriteAllText(commandServiceResult);

                    var serviceInterface = _commandInterfaceBuilder.Build(projectName, subCommand, currentPath);
                    var commandServiceInterface = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"I{subCommand.Name.FirstCharToUpper()}Service.cs"));
                    commandServiceInterface.WriteAllText(serviceInterface);

                    commandTypeCollector.Add(subCommand, new TypeToRegister($"{commandServiceInterface.FileNameWithoutExtension()}", $"{commandService.FileNameWithoutExtension()}"));
                }


                Invoke(projectName, subCommand, subCommnandDirectoryInfo, commandTypeCollector, currentPath, namespaceCollector);
            }
        }
    }
}