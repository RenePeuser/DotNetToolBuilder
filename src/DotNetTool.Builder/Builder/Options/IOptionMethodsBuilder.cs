namespace DotNetTool.Builder.Builder.Options
{
    using System.Collections.Generic;
    using Models;

    internal interface IOptionMethodsBuilder
    {
        IEnumerable<MethodInfo> Build(IEnumerable<OptionInfo> options);
    }
}