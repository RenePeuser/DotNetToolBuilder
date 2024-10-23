using System.IO;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using DotNetTool.Builder.SolutionBuilder.Argument;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal sealed class CreateArgumentStructure(IDirectoryService directoryService,
                                                  IFileService fileService,
                                                  IArgumentInterfaceBuilder argumentInterfaceBuilder,
                                                  IArgumentBuilder argumentBuilder,
                                                  ITypeService typeService)
        : IBuildCommandFileStructure
    {
        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.Argument.IsNull())
            {
                return;
            }

            var argumentFolderPath = Path.Combine(subCommnandDirectoryInfo.FullName, "Arguments");
            var argumentFolder = directoryService.CreateDirectory(argumentFolderPath);

            var argumentInterfaceSyntaxTree = argumentInterfaceBuilder.Build(projectName, subCommand, currentPath);
            var argumentInterfaceFilePath = fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"I{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentInterfaceFilePath.FullName, argumentInterfaceSyntaxTree);

            var argumentImplementationSyntaxTree = argumentBuilder.Build(projectName, subCommand, currentPath);
            var argumentImplementationFilePath = fileService.GetFileInfo(Path.Combine(argumentFolder.FullName, $"{subCommand.NormalizedName}ArgumentBuilder.cs"));
            File.WriteAllText(argumentImplementationFilePath.FullName, argumentImplementationSyntaxTree);

            var interfaceToRegister = typeService.GetFullQualifiedName(projectName, argumentInterfaceFilePath);
            var implementationToRegister = typeService.GetFullQualifiedName(projectName, argumentImplementationFilePath);

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceToRegister, implementationToRegister));

            namespaceCollector.Add($"{currentPath}.Arguments");
        }
    }
}
