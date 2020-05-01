using System.Collections.Generic;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Options
{
    internal interface IOptionMethodsBuilder
    {
        IEnumerable<MethodInfo> Build(IEnumerable<OptionInfo> options);
    }
}
