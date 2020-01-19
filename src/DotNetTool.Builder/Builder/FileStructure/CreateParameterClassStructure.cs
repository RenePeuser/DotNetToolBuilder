using System.IO;
using System.Linq;
using DotNetTool.Builder.Builder.Parameter;

using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Collectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.Builder.FileStructure
{
    internal class CreateParameterClassStructure : IBuildCommandFileStructure
    {
        private readonly IFileService _fileService;
        private readonly IParameterClassBuilder _parameterClassBuilder;

        public CreateParameterClassStructure(
            IFileService fileService,
            IParameterClassBuilder parameterClassBuilder)
        {
            _fileService = fileService;
            _parameterClassBuilder = parameterClassBuilder;
        }

        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (!subCommand.Argument.IsNotNull() && !subCommand.Options.Any() && !subCommand.SubCommands.IsNullOrEmpty())
            {
                return;
            }

            var parameterModelClass = _parameterClassBuilder.Build(projectName, subCommand, currentPath);
            var parameterClassFileInfo = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}Parameters.cs"));
            parameterClassFileInfo.WriteAllText(parameterModelClass);
            namespaceCollector.Add($"{currentPath}.Service");
        }
    }
}
