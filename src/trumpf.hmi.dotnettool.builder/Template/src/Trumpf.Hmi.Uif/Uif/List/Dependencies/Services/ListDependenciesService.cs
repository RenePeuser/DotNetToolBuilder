namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

    public class ListDependenciesService : IListDependenciesService
    {
        private readonly IEnumerable<IListDependenciesStrategy> _listStrategies;

        public ListDependenciesService(IListDependenciesStrategy[] listStrategies)
        {
            _listStrategies = listStrategies;
        }

        public Task HandleAsync(ListOptions options, TiFileInfo tcixFile)
        {
            var strategy = _listStrategies.Single(s => s.IsStrategyFor(options));
            return strategy.Invoke(tcixFile);
        }
    }
}