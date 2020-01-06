using System.IO;
using System.Linq;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Collectors;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Builder.FileStructure
{
    internal class CommandStructureBuilder : IBuildCommandFileStructure
    {
        private readonly ICommandBuilderSimple _commandBuilderSimple;
        private readonly ICommandBuilderWithArgument _commandBuilderWithArgument;
        private readonly ICommandBuilderWithArgumentAndOption _commandBuilderWithArgumentAndOption;
        private readonly ICommandBuilderWithOptions _commandBuilderWithOptions;
        private readonly IFileService _fileService;
        private readonly ITypeService _typeService;

        public CommandStructureBuilder(
            ICommandBuilderSimple commandBuilderSimple,
            ICommandBuilderWithOptions commandBuilderWithOptions,
            ICommandBuilderWithArgument commandBuilderWithArgument,
            ICommandBuilderWithArgumentAndOption commandBuilderWithArgumentAndOption,
            IFileService fileService,
            ITypeService typeService)
        {
            _commandBuilderSimple = commandBuilderSimple;
            _commandBuilderWithOptions = commandBuilderWithOptions;
            _commandBuilderWithArgument = commandBuilderWithArgument;
            _commandBuilderWithArgumentAndOption = commandBuilderWithArgumentAndOption;
            _fileService = fileService;
            _typeService = typeService;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.SubCommands.IsNotNull() && subCommand.SubCommands.Any())
            {
                return;
            }

            string command = null;
            if (subCommand.Argument.IsNull() && subCommand.Options.IsEmpty())
            {
                command = _commandBuilderSimple.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNull() && subCommand.Options.Any())
            {
                command = _commandBuilderWithOptions.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNotNull() && subCommand.Options.IsEmpty())
            {
                command = _commandBuilderWithArgument.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNotNull() && subCommand.Options.Any())
            {
                command = _commandBuilderWithArgumentAndOption.Build(projectName, subCommand, parameter, currentPath);
            }

            var fileInfo = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}CommandBuilder.cs"));
            fileInfo.WriteAllText(command);


            var splittedNamespace = currentPath.Split(".").ToList();
            splittedNamespace.Remove(splittedNamespace.Last());
            var newNamespaceForInterface = splittedNamespace.Flatten(".");

            var interfaceName = $"{newNamespaceForInterface}.I{parameter.NormalizedName}SubCommandBuilder";
            var implementationToRegister = _typeService.GetFullQualifiedName(projectName, fileInfo);


            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceName, implementationToRegister));
        }
    }
}
