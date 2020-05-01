using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Argument
{
    internal interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
