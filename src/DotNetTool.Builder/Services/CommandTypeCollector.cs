using System.Collections.Generic;
using DotNetTool.Builder.Models;
using Trumpf.Hmi.Extensions;

namespace DotNetTool.Builder.Services
{
    public class CommandTypeCollector : ICommandTypeCollector
    {
        private readonly Dictionary<string, IEnumerable<TypeToRegister>> _typesToregister;

        public CommandTypeCollector()
        {
            _typesToregister = new Dictionary<string, IEnumerable<TypeToRegister>>();
        }

        public void Add(ParameterInfo parameterInfo, TypeToRegister typeToRegister)
        {
            var name = parameterInfo.Name;
            var alreadyExists = _typesToregister.ContainsKey(name);
            if (alreadyExists)
            {
                _typesToregister[name] = _typesToregister[name].Concat(typeToRegister);
            }
            else
            {
                _typesToregister[name] = typeToRegister.ToIList();
            }
        }

        public Dictionary<string, IEnumerable<TypeToRegister>> GetAll()
        {
            return _typesToregister;
        }
    }
}