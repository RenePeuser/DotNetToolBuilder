using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandBuilderWithOptions
    {
        string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace);
    }
}