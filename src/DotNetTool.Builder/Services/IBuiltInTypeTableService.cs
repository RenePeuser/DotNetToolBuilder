using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface IBuiltInTypeTableService
    {
        TypeToAlias GetTypeFor(string typeName);
    }
}