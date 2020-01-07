using System.Runtime.CompilerServices;
using DotNetTool.Builder.Extensions;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("DotNetTool.Builder.Test")]

namespace DotNetTool.Builder.Models
{
    public class ArgumentInfo : InfoBase
    {
        private string _optimizedType;

        [JsonConstructor]
        public ArgumentInfo(string name, string description, string value, string type, string optimizedType) : base(value, name)
        {
            Description = description;
            Type = type;
            _optimizedType = optimizedType;
        }

        public string Description { get; }

        public string Type { get; }

        // ToDo: make it clean, this is to hold compatibility to older serialized tools.
        // Fast workaround to keep compatibility
        public string OptimizedType
        {
            get => _optimizedType.IsNull() ? Type : _optimizedType;
        }
    }
}
