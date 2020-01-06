using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.Dotnet.Newtool.Options
{
    internal interface INewToolOptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}
