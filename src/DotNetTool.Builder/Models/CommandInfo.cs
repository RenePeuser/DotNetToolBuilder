using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class CommandInfo : InfoBase
    {
        public CommandInfo(string value, string name, string normalizedName, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options) : this(value, name, normalizedName, description, argumentInfo, options, Enumerable.Empty<CommandInfo>())
        {
        }

        [JsonConstructor]
        public CommandInfo(string value, string name, string normalizedName, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, IEnumerable<CommandInfo> subCommands) : base(value, name, normalizedName)
        {
            Description = description;
            Argument = argumentInfo;
            Options = options;
            SubCommands = subCommands;
        }

        public IEnumerable<CommandInfo> SubCommands { get; }

        public IEnumerable<OptionInfo> Options { get; }

        public ArgumentInfo Argument { get; }

        public string Description { get; }
    }
}
