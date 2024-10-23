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
    internal sealed class CommandStructureBuilder(ICommandBuilderSimple commandBuilderSimple,
                                                  ICommandBuilderWithOptions commandBuilderWithOptions,
                                                  ICommandBuilderWithArgument commandBuilderWithArgument,
                                                  ICommandBuilderWithArgumentAndOption commandBuilderWithArgumentAndOption,
                                                  IFileService fileService,
                                                  ITypeService typeService)
        : IBuildCommandFileStructure
    {
        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (subCommand.SubCommands.IsNotNull() && subCommand.SubCommands.Any())
            {
                return;
            }

            string command = null;
            if (subCommand.Argument.IsNull() && subCommand.Options.IsEmpty())
            {
                command = commandBuilderSimple.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNull() && subCommand.Options.Any())
            {
                command = commandBuilderWithOptions.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNotNull() && subCommand.Options.IsEmpty())
            {
                command = commandBuilderWithArgument.Build(projectName, subCommand, parameter, currentPath);
            }
            else if (subCommand.Argument.IsNotNull() && subCommand.Options.Any())
            {
                command = commandBuilderWithArgumentAndOption.Build(projectName, subCommand, parameter, currentPath);
            }

            var fileInfo = fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}CommandBuilder.cs"));
            fileInfo.WriteAllText(command);


            var splittedNamespace = currentPath.Split(".").ToList();
            splittedNamespace.Remove(splittedNamespace.Last());
            var newNamespaceForInterface = splittedNamespace.Flatten(".");

            var interfaceName = $"{newNamespaceForInterface}.I{parameter.NormalizedName}SubCommandBuilder";
            var implementationToRegister = typeService.GetFullQualifiedName(projectName, fileInfo);


            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceName, implementationToRegister));
        }
    }
}
