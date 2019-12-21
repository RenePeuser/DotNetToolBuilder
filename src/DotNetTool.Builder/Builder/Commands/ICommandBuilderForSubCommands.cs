using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandBuilderForSubCommands
    {
        string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace);
    }
}