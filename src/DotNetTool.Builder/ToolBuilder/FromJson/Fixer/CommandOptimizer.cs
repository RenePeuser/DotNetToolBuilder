using DotNetTool.Builder.Models;
using DotNetTool.Builder.Services.Optimizer;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class CommandOptimizer : ICommandFixer
    {
        private readonly IDotNetToolNameNormalizer _dotNetToolNameNormalizer;

        public CommandOptimizer(IDotNetToolNameNormalizer dotNetToolNameNormalizer )
        {
            _dotNetToolNameNormalizer = dotNetToolNameNormalizer;
        }

        public CommandInfo Optimize(CommandInfo commandInfo)
        {
            commandInfo.NormalizedName = _dotNetToolNameNormalizer.Normalize(commandInfo.Name);
            return commandInfo;
        }
    }
}