using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandInterfaceBuilder
    {
        string Build(string project, CliParameterInfo cliParameterInfo, string nameSpace);
    }
}