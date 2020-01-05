using System.Collections.Generic;

namespace DotNetTool.Builder.Services
{
    internal interface INameSpaceCollector
    {
        void Add(string nameSpace);
        IEnumerable<string> GetAll();
    }
}
