using System.IO;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.SolutionBuilder.Options;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal class CreateOptionsStructure : IBuildCommandFileStructure
    {
        private readonly IDirectoryService _directoryService;
        private readonly IFileService _fileService;
        private readonly IOptionImplementationBuilder _optionImplementationBuilder;
        private readonly IOptionInterfaceBuilder _optionInterfaceBuilder;
        private readonly ITypeService _typeService;

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

            var interfaceToRegister = _typeService.GetFullQualifiedName(projectName, optionsInterfaceFilePath);
            var implementationToRegister = _typeService.GetFullQualifiedName(projectName, optionsImplementationFilePath);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));

            namespaceCollector.Add($"{currentPath}.Options");
        }
    }
}
