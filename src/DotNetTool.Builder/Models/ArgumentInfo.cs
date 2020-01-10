using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    public class ArgumentInfo : InfoBase
    {
        [JsonConstructor]
        public ArgumentInfo(string name, string description, string value, string type, string optimizedType, string normalizedName) : base(value, name, normalizedName)
        {
            Description = description;
            Type = type;
            OptimizedType = optimizedType;
        }

        public string Description { get; }

        public string Type { get; }

        public string OptimizedType { get; }
    }
}
