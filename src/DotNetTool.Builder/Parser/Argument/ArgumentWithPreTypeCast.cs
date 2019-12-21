using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Argument
{
    public class ArgumentWithPreTypeCast : IArgumentParser
    {
        private readonly IArgumentOnly _argumentOnly;

        public ArgumentWithPreTypeCast(IArgumentOnly argumentOnly)
        {
            _argumentOnly = argumentOnly;
        }

        public bool IsThisParserFor(string value)
        {
            return value.StartsWith("[");
        }

        public ArgumentInfo Parse(string value)
        {
            var parameterAndTypeInfo = value.Split("]");
            var current = parameterAndTypeInfo.Last();
            var typeInfo = parameterAndTypeInfo.Length > 1 ? parameterAndTypeInfo.First().Trim('[') : "object";

            var argument = _argumentOnly.Parse(current);

            return new ArgumentInfo(argument.Name, argument.Description, argument.Value, argument.NormalizedName, typeInfo);
        }
    }
}