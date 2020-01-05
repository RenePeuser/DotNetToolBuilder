using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    internal interface IArgumentBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
