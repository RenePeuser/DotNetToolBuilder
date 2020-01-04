using System.IO;
using DotNetTool.Builder.Builder.Parameter;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.FileStructure
{
    internal class CreateParameterClassStructure : ICreateParameterClassStructure
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

        public void Create(string projectName, string currentPath, INameSpaceCollector namespaceCollector, CommandInfo subCommand, IDirectoryInfo subCommnandDirectoryInfo)
        {
            var parameterModelClass = _parameterClassBuilder.Build(projectName, subCommand, currentPath);
            var parameterClassFileInfo = _fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}Parameters.cs"));
            parameterClassFileInfo.WriteAllText(parameterModelClass);
            namespaceCollector.Add($"{currentPath}.Service");
        }
    }
}