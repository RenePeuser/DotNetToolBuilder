using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal interface ICommandTypeCollector
    {
        void Add(CommandInfo parameterInfo, TypeToRegister typeToRegister);

        Dictionary<string, IEnumerable<TypeToRegister>> GetAll();
    }
}
