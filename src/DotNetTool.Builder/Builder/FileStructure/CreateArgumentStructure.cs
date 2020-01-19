using System.IO;
using DotNetTool.Builder.Builder.Argument;

using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.Services.Collectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Builder.FileStructure
{
    internal class CreateArgumentStructure : IBuildCommandFileStructure
    {
        private readonly IArgumentBuilder _argumentBuilder;
        private readonly IArgumentInterfaceBuilder _argumentInterfaceBuilder;
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly ITypeService _typeService;

        public CreateArgumentStructure(IDirectoryService directoryService, IFileService fileService, IArgumentInterfaceBuilder argumentInterfaceBuilder, IArgumentBuilder argumentBuilder, ITypeService typeService)
        {
            _directoryService = directoryService;
            _fileService = fileService;
            _argumentInterfaceBuilder = argumentInterfaceBuilder;
            _argumentBuilder = argumentBuilder;
            _typeService = typeService;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.Argument.IsNull())
            {
                return;
            }

            var argumentFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Arguments");
            var argumentFolder = _directoryService.CreateDirectory(argumentFolderPath);

            var argumentInterfaceSyntaxTree = _argumentInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var argumentInterfaceFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"I{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentInterfaceFilePath.FullName, argumentInterfaceSyntaxTree);

            var argumentImplementationSyntaxTree = _argumentBuilder.Build(projectName, subCommand, currentPath);
            var argumentImplementationFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentImplementationFilePath.FullName, argumentImplementationSyntaxTree);

            var interfaceToRegister = _typeService.GetFullQualifiedName(projectName, argumentInterfaceFilePath);
            var implementationToRegister = _typeService.GetFullQualifiedName(projectName, argumentImplementationFilePath);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));

            namespaceCollector.Add($"{currentPath}.Arguments");
        }
    }
}
