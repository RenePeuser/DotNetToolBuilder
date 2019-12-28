using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandServiceInterfaceBuilder
    {
        string Build(string project, ParameterInfo parameterInfo, string nameSpace);
    }
}
