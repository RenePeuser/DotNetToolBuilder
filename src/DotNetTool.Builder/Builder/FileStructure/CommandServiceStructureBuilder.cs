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
    internal class CommandServiceStructureBuilder : IBuildCommandFileStructure
    {
        private readonly ICommandServiceBuilder _commandServiceBuilder;
        private readonly ICommandServiceInterfaceBuilder _commandServiceInterfaceBuilder;
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly ITypeService _typeService;

        public CommandServiceStructureBuilder(
            IDirectoryService directoryService,
            IFileService fileService,
            ICommandServiceBuilder commandServiceBuilder,
            ICommandServiceInterfaceBuilder commandServiceInterfaceBuilder,
            ITypeService typeService)
        {
            _directoryService = directoryService;
            _fileService = fileService;
            _commandServiceBuilder = commandServiceBuilder;
            _commandServiceInterfaceBuilder = commandServiceInterfaceBuilder;
            _typeService = typeService;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.SubCommands.IsNotNull() && subCommand.SubCommands.Any())
            {
                return;
            }

            var commandServiceResult = _commandServiceBuilder.Build(projectName, subCommand, currentPath);
            var serviceFolder = _directoryService.GetDirectoryInfo(Path.Combine(subCommnandDirectoryInfo.FullName, "Service"));
            serviceFolder.Exists.IfFalseThen(() => serviceFolder.Create());
            var commandService = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"{subCommand.NormalizedName}Service.cs"));
            commandService.WriteAllText(commandServiceResult);

            var serviceInterface = _commandServiceInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var commandServiceInterface = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"I{subCommand.NormalizedName}Service.cs"));
            commandServiceInterface.WriteAllText(serviceInterface);

            var interfaceToRegister = _typeService.GetFullQualifiedName(projectName, commandServiceInterface);
            var implementationToRegister = _typeService.GetFullQualifiedName(projectName, commandService);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));
        }
    }
}
