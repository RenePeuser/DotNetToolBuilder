using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class DotNetToolNameFixer : IDotNetToolFromJsonFixer
    {
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public DotNetToolNameFixer(IDotNetToolNameNormalizer dotNetToolNameNormalizer)
        {
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var normalizedName = _dotNetToolNameNormalizer.Normalize(dotNetTool.DotNetToolName.Value);
            var dotNetToolName = new DotNetToolName(dotNetTool.DotNetToolName.Value, normalizedName);

            return new Models.DotNetTool(dotNetTool.ProjectName, dotNetToolName, dotNetTool.ParameterInfo);
        }
    }
}