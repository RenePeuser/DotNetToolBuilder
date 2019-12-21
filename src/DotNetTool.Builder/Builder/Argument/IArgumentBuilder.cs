using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    public interface IArgumentBuilder
    {
        string Build(string projectName, CliParameterInfo parameterInfo, string nameSpace);
    }
}