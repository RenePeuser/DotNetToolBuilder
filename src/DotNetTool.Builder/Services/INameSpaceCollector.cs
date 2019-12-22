using System.Collections.Generic;

namespace DotNetTool.Builder.Services
{
    public interface INameSpaceCollector
    {
        void Add(string nameSpace);
        IEnumerable<string> GetAll();
    }
}
