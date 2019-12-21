using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandBuilderWithArgumentAndOption
    {
        string Build(string project, CliParameterInfo cliParameterInfo, CliParameterInfo parent, string nameSpace);
    }
}