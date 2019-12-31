using System.Collections.Generic;
using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Services
{
    public class CommandTypeCollector : ICommandTypeCollector
    {
        private readonly Dictionary<string, IEnumerable<TypeToRegister>> _typesToRegister;

        public CommandTypeCollector()
        {
            _typesToRegister = new Dictionary<string, IEnumerable<TypeToRegister>>();
        }

        public void Add(CommandInfo parameterInfo, TypeToRegister typeToRegister)
        {
            var name = parameterInfo.Name;
            var alreadyExists = _typesToRegister.ContainsKey(name);
            if (alreadyExists)
            {
                _typesToRegister[name] = _typesToRegister[name].Concat(typeToRegister);
            }
            else
            {
                _typesToRegister[name] = typeToRegister.ToIList();
            }
        }

        public Dictionary<string, IEnumerable<TypeToRegister>> GetAll()
        {
            return _typesToRegister;
        }
    }
}
