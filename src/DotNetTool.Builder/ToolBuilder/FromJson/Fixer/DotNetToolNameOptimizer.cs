using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class DotNetToolNameOptimizer
    {
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public DotNetToolNameOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public DotNetToolName Optimize(DotNetToolName dotNetToolName)
        {
            var normalizedName = _dotNetToolNameNormalizer.Normalize(dotNetToolName.Value);

            return new DotNetToolName(dotNetToolName.Value, normalizedName);
        }
    }
}