using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Parameter
{
    internal interface IConstructorArgumentBuilder
    {
        IEnumerable<CtorArgument> Build(CommandInfo parameterInfo);
    }
}
