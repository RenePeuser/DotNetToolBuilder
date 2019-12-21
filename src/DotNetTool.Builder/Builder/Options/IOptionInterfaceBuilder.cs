using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public interface IOptionInterfaceBuilder
    {
        string Build(string projectName, CliParameterInfo parameterInfo, string nameSpace);
    }
}