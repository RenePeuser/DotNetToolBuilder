using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandServiceBuilder
    {
        string Build(string project, CommandInfo parameterInfo, string nameSpace);
    }
}
