using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Parameter
{
    internal interface IConstructorArgumentBuilder
    {
        IEnumerable<CtorArgument> Build(CommandInfo parameterInfo);
    }
}
