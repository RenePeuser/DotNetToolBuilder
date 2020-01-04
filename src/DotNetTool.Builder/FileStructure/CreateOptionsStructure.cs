using System.IO;
using DotNetTool.Builder.Builder.Options;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateOptionsStructure : IBuildCommandFileStructure
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly IOptionImplementationBuilder _optionImplementationBuilder;
        private readonly ITypeService _typeService;
        private readonly IOptionInterfaceBuilder _optionInterfaceBuilder;

        public CreateOptionsStructure(
            IOptionInterfaceBuilder optionInterfaceBuilder,
            IDirectoryService directoryService,
            IFileService fileService,
            IOptionImplementationBuilder optionImplementationBuilder,
            ITypeService typeService)
        {
            _optionInterfaceBuilder = optionInterfaceBuilder;
            _directoryService = directoryService;
            _fileService = fileService;
            _optionImplementationBuilder = optionImplementationBuilder;
            _typeService = typeService;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.Options.IsNullOrEmpty())
            {
                return;
            }

            var optionFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Options");
            var optionFolder = _directoryService.CreateDirectory(optionFolderPath);

            var optionsInterfaceSyntaxTree = _optionInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var optionsInterfaceFilePath = _fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"I{subCommand.NormalizedName}OptionsBuilder.cs"));

            File.WriteAllText(optionsInterfaceFilePath.FullName, optionsInterfaceSyntaxTree);

            var optionsImplementationSyntaxTree = _optionImplementationBuilder.Build(projectName, subCommand, currentPath);
            var optionsImplementationFilePath = _fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"{subCommand.NormalizedName}OptionsBuilder.cs"));
            File.WriteAllText(optionsImplementationFilePath.FullName, optionsImplementationSyntaxTree);

            var interfaceToRegister = _typeService.GetFullqualifiedName(projectName, optionsInterfaceFilePath);
            var implementationToRegister = _typeService.GetFullqualifiedName(projectName, optionsImplementationFilePath);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));

            namespaceCollector.Add($"{currentPath}.Options");
        }
    }
}