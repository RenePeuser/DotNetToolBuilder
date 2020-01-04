using System.IO;
using DotNetTool.Builder.Builder.Argument;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateArgumentStructure : ICreateArgumentStructure
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly IArgumentInterfaceBuilder _argumentInterfaceBuilder;
        private readonly IArgumentBuilder _argumentBuilder;

        public CreateArgumentStructure(IDirectoryService directoryService, IFileService fileService, IArgumentInterfaceBuilder argumentInterfaceBuilder, IArgumentBuilder argumentBuilder)
        {
            _directoryService = directoryService;
            _fileService = fileService;
            _argumentInterfaceBuilder = argumentInterfaceBuilder;
            _argumentBuilder = argumentBuilder;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            var argumentFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Arguments");
            var argumentFolder = _directoryService.CreateDirectory(argumentFolderPath);

            var argumentInterfaceSyntaxTree = _argumentInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var argumentInterfaceFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"I{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentInterfaceFilePath.FullName, argumentInterfaceSyntaxTree);

            var argumentImplementationSyntaxTree = _argumentBuilder.Build(projectName, subCommand, currentPath);
            var argumentImplementationFilePath = _fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentImplementationFilePath.FullName, argumentImplementationSyntaxTree);

            commandTypeCollector.Add(parameter, new TypeToRegister(argumentInterfaceFilePath.FileNameWithoutExtension(), argumentImplementationFilePath.FileNameWithoutExtension()));

            namespaceCollector.Add($"{currentPath}.Arguments");
        }
    }
}