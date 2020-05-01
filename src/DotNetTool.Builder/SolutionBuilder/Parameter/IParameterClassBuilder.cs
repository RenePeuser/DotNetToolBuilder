using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Parameter
{
    internal interface IParameterClassBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
