using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandInterfaceBuilder
    {
        string Build(string project, ParameterInfo parameterInfo, string nameSpace);
    }
}