using System.Runtime.CompilerServices;
using DotNetTool.Builder.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    using Newtonsoft.Json;
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    internal class CommandInfo : InfoBase
    {
        public IEnumerable<CommandInfo> SubCommands { get; }

        public IEnumerable<OptionInfo> Options { get; }

        public ArgumentInfo Argument { get; }

        public string AsArgumentName => Name.FirstCharToLower();

        public string Description { get; }

        internal CommandInfo(string value, string name, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options) : this(value, name, description, argumentInfo, options, Enumerable.Empty<CommandInfo>())
        {
        }

        [JsonConstructor]
        internal CommandInfo(string value, string name, string description, ArgumentInfo argumentInfo, IEnumerable<OptionInfo> options, IEnumerable<CommandInfo> subCommands) : base(value, name)
        {
            Description = description;
            Argument = argumentInfo;
            Options = options;
            SubCommands = subCommands;
        }
    }
}
