using System.IO;
using System.Linq;
using DotNetTool.Builder.Builder.Commands;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateSubCommandStructure : IBuildCommandFileStructure
    {
        private readonly ICommandBuilderForSubCommands _commandBuilderForSubCommands;
        private readonly IFileService _fileService;
        private readonly ISubCommandInterfaceBuilder _subCommandInterfaceBuilder;

        public CreateSubCommandStructure(
            ICommandBuilderForSubCommands commandBuilderForSubCommands,
            ISubCommandInterfaceBuilder subCommandInterfaceBuilder,
            IFileService fileService)
        {
            _commandBuilderForSubCommands = commandBuilderForSubCommands;
            _subCommandInterfaceBuilder = subCommandInterfaceBuilder;
            _fileService = fileService;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (!subCommand.SubCommands.IsNotNull() || !subCommand.SubCommands.Any())
            {
                return;
            }

            var result = _commandBuilderForSubCommands.Build(projectName, subCommand, parameter, currentPath);
            var filePath0 = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}CommandBuilder.cs"));
            filePath0.WriteAllText(result);
            
            var subCommandBuilder = _subCommandInterfaceBuilder.Build(projectName, subCommand, parameter, currentPath);
            var filePath1 = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"I{subCommand.NormalizedName}SubCommandBuilder.cs"));
            filePath1.WriteAllText(subCommandBuilder);

            commandTypeCollector.Add(subCommand, new TypeToRegister($"I{parameter.NormalizedName}SubCommandBuilder", filePath0.FileNameWithoutExtension()));
        }
    }
}