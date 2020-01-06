using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services.Collectors
{
    internal interface ICommandTypeCollector
    {
        void Add(CommandInfo parameterInfo, TypeToRegister typeToRegister);

        Dictionary<string, IEnumerable<TypeToRegister>> GetAll();
    }
}
