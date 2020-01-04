using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.Dotnet.Newtool.Options
{
    public interface INewToolOptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}