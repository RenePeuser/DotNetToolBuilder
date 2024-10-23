using System.IO;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.SolutionBuilder.Options;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal sealed class CreateOptionsStructure(IOptionInterfaceBuilder optionInterfaceBuilder,
                                                 IDirectoryService directoryService,
                                                 IFileService fileService,
                                                 IOptionImplementationBuilder optionImplementationBuilder,
                                                 ITypeService typeService)
        : IBuildCommandFileStructure
    {
        public void Create(string projectName,
                           CommandInfo parameter,
                           ICommandTypeCollector commandTypeCollector,
                           string currentPath,
                           INameSpaceCollector namespaceCollector,
                           IDirectoryInfo subCommnandDirectoryInfo,
                           CommandInfo subCommand)
        {
            if (subCommand.Options.IsNullOrEmpty())
            {
                return;
            }

            var optionFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Options");
            var optionFolder = directoryService.CreateDirectory(optionFolderPath);

            var optionsInterfaceSyntaxTree = optionInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var optionsInterfaceFilePath = fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"I{subCommand.NormalizedName}OptionsBuilder.cs"));

            File.WriteAllText(optionsInterfaceFilePath.FullName, optionsInterfaceSyntaxTree);

            var optionsImplementationSyntaxTree = optionImplementationBuilder.Build(projectName, subCommand, currentPath);
            var optionsImplementationFilePath = fileService.GetFileInfo(Path.Combine(optionFolder.FullName, $"{subCommand.NormalizedName}OptionsBuilder.cs"));
            File.WriteAllText(optionsImplementationFilePath.FullName, optionsImplementationSyntaxTree);

            var interfaceToRegister = typeService.GetFullQualifiedName(projectName, optionsInterfaceFilePath);
            var implementationToRegister = typeService.GetFullQualifiedName(projectName, optionsImplementationFilePath);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));

            namespaceCollector.Add($"{currentPath}.Options");
        }
    }
}