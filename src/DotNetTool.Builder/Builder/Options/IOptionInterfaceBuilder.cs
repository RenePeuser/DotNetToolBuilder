using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public interface IOptionInterfaceBuilder
    {
        string Build(string projectName, ParameterInfo parameterInfo, string nameSpace);
    }
}