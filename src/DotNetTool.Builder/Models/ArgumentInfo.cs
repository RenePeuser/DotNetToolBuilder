using System.Runtime.CompilerServices;
using DotNetTool.Builder.Extensions;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    internal class ArgumentInfo : InfoBase
    {
        [JsonConstructor]
        internal ArgumentInfo(string name, string description, string value, string normalizedName, string type) : base(value, name)
        {
            Description = description;
            NormalizedParameterName = normalizedName.FirstCharToLower();
            Type = type;
        }

        public string Description { get; }

        public string NormalizedParameterName { get; }

        public string Type { get; }
    }
}
