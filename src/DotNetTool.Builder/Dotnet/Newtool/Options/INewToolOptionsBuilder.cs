using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.DotNet.Newtool.Options
{
    internal interface INewToolOptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}
