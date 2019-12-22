using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Argument
{
    public interface IArgumentBuilder
    {
        string Build(string projectName, ParameterInfo parameterInfo, string nameSpace);
    }
}
