using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    internal interface ICommandTypeCollector
    {
        void Add(CommandInfo parameterInfo, TypeToRegister typeToRegister);

        Dictionary<string, IEnumerable<TypeToRegister>> GetAll();
    }
}
