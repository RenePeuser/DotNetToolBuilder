using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public interface ICommandTypeCollector
    {
        void Add(ParameterInfo parameterInfo, TypeToRegister typeToRegister);
        Dictionary<string, IEnumerable<TypeToRegister>> GetAll();
    }
}
