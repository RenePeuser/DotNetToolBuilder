using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    internal class ArgumentInfo : InfoBase
    {
        [JsonConstructor]
        internal ArgumentInfo(string name, string description, string value, string type) : base(value, name)
        {
            Description = description;
            Type = type;
        }

        internal string Description { get; }


        internal string Type { get; }
    }
}
