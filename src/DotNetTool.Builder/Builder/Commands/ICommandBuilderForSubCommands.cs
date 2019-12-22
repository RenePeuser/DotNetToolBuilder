using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandBuilderForSubCommands
    {
        string Build(string project, ParameterInfo parameterInfo, ParameterInfo parent, string nameSpace);
    }
}
