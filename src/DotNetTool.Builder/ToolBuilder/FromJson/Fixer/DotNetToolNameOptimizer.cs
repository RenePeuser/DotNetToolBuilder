using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal sealed class DotNetToolNameOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer)
    {
        public DotNetToolName Optimize(DotNetToolName dotNetToolName)
        {
            var normalizedName = dotNetToolNameNormalizer.Normalize(dotNetToolName.Name);

            return new DotNetToolName(dotNetToolName.Name, normalizedName);
        }
    }
}