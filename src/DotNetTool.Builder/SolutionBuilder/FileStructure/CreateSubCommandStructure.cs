using System.IO;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.SolutionBuilder.Commands;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal sealed class CreateSubCommandStructure(ICommandBuilderForSubCommands commandBuilderForSubCommands,
                                                    ISubCommandInterfaceBuilder subCommandInterfaceBuilder,
                                                    IFileService fileService) : IBuildCommandFileStructure
    {
        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (!subCommand.SubCommands.IsNotNull() || !subCommand.SubCommands.Any())
            {
                return;
            }

            var result = commandBuilderForSubCommands.Build(projectName, subCommand, parameter, currentPath);
            var filePath0 = fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}CommandBuilder.cs"));
            filePath0.WriteAllText(result);

            var subCommandBuilder = subCommandInterfaceBuilder.Build(projectName, subCommand, parameter, currentPath);
            var filePath1 = fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"I{subCommand.NormalizedName}SubCommandBuilder.cs"));
            filePath1.WriteAllText(subCommandBuilder);

            var splittedNamespace = currentPath.Split(".").ToList();
            splittedNamespace.Remove(splittedNamespace.Last());
            var newNamespaceForInterface = splittedNamespace.Flatten(".");
            var interfaceType = $"{newNamespaceForInterface}.I{parameter.NormalizedName}SubCommandBuilder";
            var implementation = $"{currentPath}.{filePath0.NameWithoutExtension}";

            commandTypeCollector.Add(subCommand, new TypeToRegister(interfaceType, implementation));
        }
    }
}
