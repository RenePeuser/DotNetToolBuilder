using System.Collections.Generic;
using System.IO;

using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Collectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Builder.FileStructure
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

                var closure = currentPath;
                _commandFileStructures.ForEach(structure => structure.Create(projectName, parameter, commandTypeCollector, closure, namespaceCollector, subCommnandDirectoryInfo, subCommand));

                Invoke(projectName, subCommand, subCommnandDirectoryInfo, commandTypeCollector, currentRootPath, namespaceCollector);
            }
        }
    }
}
