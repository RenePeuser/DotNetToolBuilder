using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Argument
{
    internal interface IArgumentBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
