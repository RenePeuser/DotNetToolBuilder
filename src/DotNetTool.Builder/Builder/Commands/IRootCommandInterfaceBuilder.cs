using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface IRootCommandInterfaceBuilder
    {
        string Build(string project, ParameterInfo parameterInfo, string nameSpace);
    }
}
