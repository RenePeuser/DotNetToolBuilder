using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface ICommandHandlerStringBuilder
    {
        string Build(ParameterInfo parameterInfo);
    }
}
