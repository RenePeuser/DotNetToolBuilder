using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Parameter
{
    internal interface IParameterSpecificClassBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
        bool IsThisBuilderFor(CommandInfo parameterInfo);
    }
}
