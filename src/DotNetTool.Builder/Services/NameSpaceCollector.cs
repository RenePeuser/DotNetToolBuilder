using System.Collections.Generic;

namespace DotNetTool.Builder.Services
{
    public class NameSpaceCollector : INameSpaceCollector
    {
        private readonly List<string> items = new List<string>();

        public void Add(string nameSpace)
        {
            items.Add(nameSpace);
        }

        public IEnumerable<string> GetAll()
        {
            return items;
        }
    }
}