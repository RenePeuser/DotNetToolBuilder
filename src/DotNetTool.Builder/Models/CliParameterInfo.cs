using System.Collections.Generic;
using System.Linq;

namespace DotNetTool.Builder.Models
{
    public class CliParameterInfo
    {
        public IEnumerable<CliParameterInfo> SubCommands { get; set; }

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public ArgumentInfo ArgumentInfo { get; set; }

        public string Name { get; set; }

        public string Decsription { get; set; }
    }
}