using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal interface IRootCommandBuilder
    {
        string Build(string project, ParameterInfo parameterInfo, string nameSpace);
    }
}
