using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal interface IOptionImplementationBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
