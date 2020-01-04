using System.IO;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CommandServiceStructureBuilder : ICommandServiceStructureBuilder
    {
        private readonly ICommandServiceInterfaceBuilder _commandServiceInterfaceBuilder;
        private readonly ICommandServiceBuilder _commandServiceBuilder;
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;

        public CommandServiceStructureBuilder(
            IDirectoryService directoryService,
            IFileService fileService,
            ICommandServiceBuilder commandServiceBuilder,
            ICommandServiceInterfaceBuilder commandServiceInterfaceBuilder)
        {
            _directoryService = directoryService;
            _fileService = fileService;
            _commandServiceBuilder = commandServiceBuilder;
            _commandServiceInterfaceBuilder = commandServiceInterfaceBuilder;
        }

        public void Create(string projectName, ICommandTypeCollector commandTypeCollector, string currentPath, CommandInfo subCommand, IDirectoryInfo subCommnandDirectoryInfo)
        {
            var commandServiceResult = _commandServiceBuilder.Build(projectName, subCommand, currentPath);
            var serviceFolder = _directoryService.GetDirectoryInfo(Path.Combine(subCommnandDirectoryInfo.FullName, "Service"));
            serviceFolder.Exists.IfFalseThen(() => serviceFolder.Create());
            var commandService = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"{subCommand.NormalizedName}Service.cs"));
            commandService.WriteAllText(commandServiceResult);

            var serviceInterface = _commandServiceInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var commandServiceInterface = _fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"I{subCommand.NormalizedName}Service.cs"));
            commandServiceInterface.WriteAllText(serviceInterface);

            commandTypeCollector.Add(subCommand, new TypeToRegister($"{commandServiceInterface.FileNameWithoutExtension()}", $"{commandService.FileNameWithoutExtension()}"));
        }
    }
}