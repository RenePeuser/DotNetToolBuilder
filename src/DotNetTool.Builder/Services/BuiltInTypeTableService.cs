using System.Collections.Generic;
using System.Linq;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal class BuiltInTypeTableService : IBuiltInTypeTableService
    {
        // The following table shows the keywords for built-in C# types, which are aliases of predefined types in the System namespace
        // All this types will bot be found as type in the system namespace so we need this information too.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table
        private static readonly List<TypeToAlias> BuiltInTypeTable = new List<TypeToAlias>
        {
            new TypeToAlias(typeof(bool), "bool"),
            new TypeToAlias(typeof(byte), "byte"),
            new TypeToAlias(typeof(sbyte), "sbyte"),
            new TypeToAlias(typeof(char), "char"),
            new TypeToAlias(typeof(decimal), "decimal"),
            new TypeToAlias(typeof(double), "double"),
            new TypeToAlias(typeof(float), "float"),
            new TypeToAlias(typeof(int), "int"),
            new TypeToAlias(typeof(uint), "uint"),
            new TypeToAlias(typeof(long), "long"),
            new TypeToAlias(typeof(ulong), "ulong"),
            new TypeToAlias(typeof(object), "object"),
            new TypeToAlias(typeof(short), "short"),
            new TypeToAlias(typeof(ushort), "ushort"),
            new TypeToAlias(typeof(string), "string")
        };

        public TypeToAlias GetTypeFor(string typeName)
        {
            var result = BuiltInTypeTable.FirstOrDefault(table => table.Alias.ToLower().EqualsTo(typeName.ToLower()));
            if (result.IsNotNull())
            {
                return result;
            }

            return null;
        }
    }
}
