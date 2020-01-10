using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    public class OptionInfo : InfoBase
    {
        [JsonConstructor]
        public OptionInfo(string value, string name, string alias, string description, bool required,
            ArgumentInfo argument, string normalizedValue, string argumentName) : base(value, name, normalizedValue)
        {
            Alias = alias;
            Description = description;
            Required = required;
            Argument = argument;
            NormalizedValue = normalizedValue;
            ArgumentName = argumentName;
        }

        public string Alias { get; }

        public string Description { get; }

        public bool Required { get; }

        public ArgumentInfo Argument { get; }

        public string NormalizedValue { get; }

        public string ArgumentName { get; }
    }
}
