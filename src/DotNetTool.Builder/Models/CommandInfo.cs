using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class CommandInfo : InfoBase
    {
        public IEnumerable<CommandInfo> SubCommands { get; set; } = Enumerable.Empty<CommandInfo>();

        public IEnumerable<OptionInfo> Options { get; set; } = Enumerable.Empty<OptionInfo>();

        public ArgumentInfo ArgumentInfo { get; set; }

        public string AsArgumentName => Name.FirstCharToLower();

        public string Description { get; set; }

        public CommandInfo(string value, string name) : base(value, name) 
        {
        }
    }
}
