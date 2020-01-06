using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal interface IParameterSpecificClassBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
        bool IsThisBuilderFor(CommandInfo parameterInfo);
    }
}
