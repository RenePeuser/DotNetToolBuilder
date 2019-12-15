using System.Collections.Generic;
using System.Linq;

namespace trumpf.hmi.dotnettool.builder.Models
{
    public class CliParameterInfo
    {
        public IEnumerable<CliParameterInfo> SubCommands { get; set; }

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public Argument ArgumentInfo { get; set; }

        public string Name { get; set; }

        public string Decsription { get; set; }
    }
}