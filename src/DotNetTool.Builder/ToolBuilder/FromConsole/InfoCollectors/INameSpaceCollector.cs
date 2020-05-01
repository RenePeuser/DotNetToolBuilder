using System.Collections.Generic;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal interface INameSpaceCollector
    {
        void Add(string nameSpace);
        IEnumerable<string> GetAll();
    }
}
