using System.IO;
using System.Linq;
using DotNetTool.Builder.Models;
using DotNetTool.Builder.SolutionBuilder.Parameter;
using DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors;
using Extensions.Pack;
using FileSystem.Abstraction;

namespace DotNetTool.Builder.SolutionBuilder.FileStructure
{
    internal sealed class CreateParameterClassStructure(IFileService fileService,
                                                        IParameterClassBuilder parameterClassBuilder) : IBuildCommandFileStructure
    {
        public void Create(string projectName, CommandInfo parameter, ICommandTypeCollector commandTypeCollector, string currentPath, INameSpaceCollector namespaceCollector, IDirectoryInfo subCommnandDirectoryInfo, CommandInfo subCommand)
        {
            if (!subCommand.Argument.IsNotNull() && !subCommand.Options.Any() && !subCommand.SubCommands.IsNullOrEmpty())
            {
                return;
            }

            var parameterModelClass = parameterClassBuilder.Build(projectName, subCommand, currentPath);
            var parameterClassFileInfo = fileService.GetFileInfo(Path.Combine(subCommnandDirectoryInfo.FullName, $"{subCommand.NormalizedName}Parameters.cs"));
            parameterClassFileInfo.WriteAllText(parameterModelClass);
            namespaceCollector.Add($"{currentPath}.Service");
        }
    }
}
