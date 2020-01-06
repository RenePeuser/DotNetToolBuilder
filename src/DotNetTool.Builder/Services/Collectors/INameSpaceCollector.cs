using System.Collections.Generic;

namespace DotNetTool.Builder.Services.Collectors
{
    internal interface INameSpaceCollector
    {
        void Add(string nameSpace);
        IEnumerable<string> GetAll();
    }
}
