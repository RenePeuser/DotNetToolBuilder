using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    internal interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}