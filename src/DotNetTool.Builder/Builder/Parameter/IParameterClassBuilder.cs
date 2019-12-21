using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal interface IParameterClassBuilder
    {
        string Build(string projectName, CliParameterInfo cliParameterInfo, string nameSpace);
    }
}