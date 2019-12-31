using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Options
{
    public interface IOptionImplementationBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}
