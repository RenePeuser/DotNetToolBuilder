using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ParameterInfo
    {
        public IEnumerable<ParameterInfo> SubCommands { get; set; } = Enumerable.Empty<ParameterInfo>();

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public ArgumentInfo ArgumentInfo { get; set; }

        public string Name { get; set; }

        public string NormalizedName => Name.FirstCharToUpper();

        public string Description { get; set; }
    }
}
