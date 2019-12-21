using System.Linq;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Parser.Argument
{
    public class ArgumentWithPostTypeCast : IArgumentParser
    {
        private readonly IArgumentOnly _argumentOnly;

        public ArgumentWithPostTypeCast(IArgumentOnly argumentOnly)
        {
            _argumentOnly = argumentOnly;
        }

        public bool IsThisParserFor(string value)
        {
            return value.EndsWith("]");
        }

        public ArgumentInfo Parse(string value)
        {
            var parameterAndTypeInfo = value.Split("[");
            var current = parameterAndTypeInfo.First();
            var typeInfo = parameterAndTypeInfo.Length > 1 ? parameterAndTypeInfo.Last().Trim(']') : "object";

            var argument = _argumentOnly.Parse(current);

            return new ArgumentInfo(argument.Name, argument.Description, value, argument.NormalizedName, typeInfo);
        }
    }
}