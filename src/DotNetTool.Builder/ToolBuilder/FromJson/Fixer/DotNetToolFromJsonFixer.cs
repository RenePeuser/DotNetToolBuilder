using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.ToolBuilder.FromJson.Fixer
{
    internal class DotNetToolFromJsonFixer
    {
        private readonly IEnumerable<IDotNetToolFromJsonFixer> _dotNetToolFromJsonFixers;

        public DotNetToolFromJsonFixer(IEnumerable<IDotNetToolFromJsonFixer> dotNetToolFromJsonFixers)
        {
            _dotNetToolFromJsonFixers = dotNetToolFromJsonFixers;
        }

        internal Models.DotNetTool FixMissingValues(Models.DotNetTool dotNetTool)
        {
            var fixedTool = _dotNetToolFromJsonFixers.Aggregate(dotNetTool, (tool, fixer) => fixer.FixMissingValues(tool));
            return fixedTool;
        }
    }
}
