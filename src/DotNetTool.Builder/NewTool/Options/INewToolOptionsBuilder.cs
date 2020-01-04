using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.NewTool.Options
{
    public interface INewToolOptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}