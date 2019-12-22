using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ISubCommandInterfaceBuilder
    {
        string Build(string project, ParameterInfo parameterInfo, ParameterInfo parent, string nameSpace);
    }
}
