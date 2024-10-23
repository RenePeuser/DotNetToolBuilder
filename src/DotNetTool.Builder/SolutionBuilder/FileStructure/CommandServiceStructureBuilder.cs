using System.IO;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.SolutionBuilder.Commands;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal sealed class CommandServiceStructureBuilder(IDirectoryService directoryService,
                                                         IFileService fileService,
                                                         ICommandServiceBuilder commandServiceBuilder,
                                                         ICommandServiceInterfaceBuilder commandServiceInterfaceBuilder,
                                                         ITypeService typeService)
        : IBuildCommandFileStructure
    {
        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.SubCommands.IsNotNull() && subCommand.SubCommands.Any())
            {
                return;
            }

            var commandServiceResult = commandServiceBuilder.Build(projectName, subCommand, currentPath);
            var serviceFolder = directoryService.GetDirectoryInfo(Path.Combine(subCommnandDirectoryInfo.FullName, "Service"));
            serviceFolder.Exists.IfFalseThen(() => serviceFolder.Create());
            var commandService = fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"{subCommand.NormalizedName}Service.cs"));
            commandService.WriteAllText(commandServiceResult);

            var serviceInterface = commandServiceInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var commandServiceInterface = fileService.GetFileInfo(Path.Combine(serviceFolder.FullName, $"I{subCommand.NormalizedName}Service.cs"));
            commandServiceInterface.WriteAllText(serviceInterface);

            var interfaceToRegister = typeService.GetFullQualifiedName(projectName, commandServiceInterface);
            var implementationToRegister = typeService.GetFullQualifiedName(projectName, commandService);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));
        }
    }
}
