using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    public class ParameterInfo
    {
        public IEnumerable<ParameterInfo> SubCommands { get; set; } = Enumerable.Empty<ParameterInfo>();

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public ArgumentInfo ArgumentInfo { get; set; }

        public string Name { get; set; }

        public string NormalizedName => NormalizedName;

        public string Description { get; set; }
    }
}