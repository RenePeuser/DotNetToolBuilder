using System.Collections.Generic;
using System.IO;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateCommandClasses : ICreateCommandClasses
    {
        private readonly IEnumerable<IBuildCommandFileStructure> _commandFileStructures;
        private readonly IDirectoryService _directoryService;

        public CreateCommandClasses(
            IDirectoryService directoryService,
            IEnumerable<IBuildCommandFileStructure> commandFileStructures)
        {
            _commandFileStructures = commandFileStructures;
            _directoryService = directoryService;
        }

        public void Invoke(string projectName, CommandInfo parameter, IDirectoryInfo rootDirectory,
            ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector)
        {
            var subCommands = parameter.SubCommands;
            if (subCommands.IsNull())
            {
                return;
            }

            var currentRootPath = $"{currentPath}.{parameter.NormalizedName}";

            foreach (var subCommand in subCommands)
            {
                currentPath = $"{currentRootPath}.{subCommand.NormalizedName}";

                namespaceCollector.Add(currentPath);
                var subCommnandDirectoryInfo = _directoryService.GetDirectoryInfo(Path.Combine(rootDirectory.FullName, subCommand.NormalizedName));
                subCommnandDirectoryInfo.Create();

                _commandFileStructures.ForEach(structue => structue.Create(projectName, parameter, commandTypeCollector, currentPath, namespaceCollector, subCommnandDirectoryInfo, subCommand));

                Invoke(projectName, subCommand, subCommnandDirectoryInfo, commandTypeCollector, currentRootPath, namespaceCollector);
            }
        }
    }
}
