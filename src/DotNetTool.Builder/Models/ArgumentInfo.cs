using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    public class ArgumentInfo : InfoBase
    {
        [JsonConstructor]
        public ArgumentInfo(string name, string description, string value, string type) : base(value, name)
        {
            Description = description;
            Type = type;
        }

        public string Description { get; }


        public string Type { get; }
    }
}
