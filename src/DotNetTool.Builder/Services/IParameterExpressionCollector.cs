using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface IParameterExpressionCollector
    {
        CliParameterInfo Collect();
    }
}