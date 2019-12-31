using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandServiceInterfaceBuilder
    {
        string Build(string project, CommandInfo parameterInfo, string nameSpace);
    }
}
