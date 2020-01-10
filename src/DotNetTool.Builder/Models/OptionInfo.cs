using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    public class OptionInfo : InfoBase
    {
        [JsonConstructor]
        public OptionInfo(string value, string name, string alias, string description, bool isRequired,
            ArgumentInfo argument, string normalizedValue) : base(value, name, normalizedValue)
        {
            Alias = alias;
            Description = description;
            IsIsRequired = isRequired;
            Argument = argument;
            NormalizedValue = normalizedValue;
        }

        public string Alias { get; }

        public string Description { get; }

        public bool IsIsRequired { get; }

        public ArgumentInfo Argument { get; }

        public string NormalizedValue { get; }
    }
}
