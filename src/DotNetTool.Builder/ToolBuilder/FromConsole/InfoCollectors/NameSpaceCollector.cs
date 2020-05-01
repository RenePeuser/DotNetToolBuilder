using System.Collections.Generic;

namespace DotNetTool.Builder.ToolBuilder.FromConsole.InfoCollectors
{
    internal class NameSpaceCollector : INameSpaceCollector
    {
        private readonly List<string> _items = new List<string>();

        public void Add(string nameSpace)
        {
            _items.Add(nameSpace);
        }

        public IEnumerable<string> GetAll()
        {
            return _items;
        }
    }
}
