using System.Collections.Generic;

namespace trumpf.hmi.dotnettool.builder
{
    public class NameSpaceCollector
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