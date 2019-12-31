using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Models
{
    using Newtonsoft.Json;

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class CommandInfo : InfoBase
    {
        public IEnumerable<CommandInfo> SubCommands { get; }

        public IEnumerable<OptionInfo> Options { get; }

        public ArgumentInfo Argument { get; }

        public string AsArgumentName => Name.FirstCharToLower();

        public string Description { get; }
        
        public CommandInfo(string value, string name, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options) : this(value, name, description, argumentInfo, options, Enumerable.Empty<CommandInfo>())
        {
        }

        [JsonConstructor]
        public CommandInfo(string value, string name, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, IEnumerable<CommandInfo> subCommands) : base(value, name)
        {
            Description = description;
            Argument = argumentInfo;
            Options = options;
            SubCommands = subCommands;
        }
    }
}
