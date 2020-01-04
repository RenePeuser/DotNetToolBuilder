using System.IO;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateCommandClasses : ICreateCommandClasses
    {
        private readonly IDirectoryService _directoryService;
        private readonly ICreateArgumentStructure _createArgumentStructure;
        private readonly ICreateOptionsStructure _createOptionsStructure;
        private readonly ICreateParameterClassStructure _createParameterClassStructure;
        private readonly ICreateSubCommandStructure _createSubCommandStructure;
        private readonly ICommandStructureBuilder _commandStructureBuilder;
        private readonly ICommandServiceStructureBuilder _commandServiceStructureBuilder;

        public CreateCommandClasses(
            IDirectoryService directoryService, 
            ICreateArgumentStructure createArgumentStructure,
            ICreateOptionsStructure createOptionsStructure, 
            ICreateParameterClassStructure createParameterClassStructure,
            ICreateSubCommandStructure createSubCommandStructure,
            ICommandStructureBuilder commandStructureBuilder,
            ICommandServiceStructureBuilder commandServiceStructureBuilder)
        {
            _directoryService = directoryService;
            _createArgumentStructure = createArgumentStructure;
            _createOptionsStructure = createOptionsStructure;
            _createParameterClassStructure = createParameterClassStructure;
            _createSubCommandStructure = createSubCommandStructure;
            _commandStructureBuilder = commandStructureBuilder;
            _commandServiceStructureBuilder = commandServiceStructureBuilder;
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

                if (subCommand.Argument.IsNotNull())
                {
                    _createArgumentStructure.Create(projectName, parameter, commandTypeCollector, currentPath, namespaceCollector, subCommnandDirectoryInfo, subCommand);
                }

                if (subCommand.Options.Any())
                {
                    _createOptionsStructure.Create(projectName, parameter, commandTypeCollector, currentPath, namespaceCollector, subCommnandDirectoryInfo, subCommand);
                }


                if (subCommand.Argument.IsNotNull() || subCommand.Options.Any() || subCommand.SubCommands.IsNullOrEmpty())
                {
                    _createParameterClassStructure.Create(projectName, currentPath, namespaceCollector, subCommand, subCommnandDirectoryInfo);
                }

                if (subCommand.SubCommands.IsNotNull() && subCommand.SubCommands.Any())
                {
                    _createSubCommandStructure.Create(projectName, parameter, commandTypeCollector, currentPath, subCommand, subCommnandDirectoryInfo);
                }
                else
                {
                    _commandStructureBuilder.Create(projectName, parameter, commandTypeCollector, currentPath, subCommand, subCommnandDirectoryInfo);
                    _commandServiceStructureBuilder.Create(projectName, commandTypeCollector, currentPath, subCommand, subCommnandDirectoryInfo);
                }

                Invoke(projectName, subCommand, subCommnandDirectoryInfo, commandTypeCollector, currentRootPath, namespaceCollector);
            }
        }
    }
}
