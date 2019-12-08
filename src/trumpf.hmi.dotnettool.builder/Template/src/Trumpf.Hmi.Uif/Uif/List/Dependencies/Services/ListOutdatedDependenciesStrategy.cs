namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Trumpf.Hmi.Extensions;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;
    using Trumpf.Hmi.Uif.Scaps.Provider;
    using Trumpf.Hmi.Uif.Tcix.Models;
    using Trumpf.Hmi.Uif.Tcix.Parser;
    using YetAnotherConsoleTables;

    public class ListOutdatedDependenciesStrategy : IListDependenciesStrategy
    {
        private readonly IScapsProvider _scapsProvider;
        private readonly ITcixParser _tcixParser;

        public ListOutdatedDependenciesStrategy(ITcixParser tcixParser, IScapsProvider scapsProvider)
        {
            _tcixParser = tcixParser;
            _scapsProvider = scapsProvider;
        }

        public async Task Invoke(TiFileInfo tcixFile)
        {
            var currentDependencies = _tcixParser.Parse(tcixFile).ToList();
            var allVersions = await _scapsProvider.GetLatestVersionsFrom(currentDependencies);

            var newest = allVersions.Where(a => a.Latest.Version.Version > a.Current.Version.Version &&  a.Current.Version.IsNotTypeOf<NoVersion>());

            var thingsToRender = newest.Select(item => new
            {
                Kind = item.Current.Kind,
                Package = item.Current.Package,
                Current = item.Current.Version.Value,
                Latest = item.Latest.Version.Value,
            });

            ConsoleTable.From(thingsToRender).Write(ConsoleTableFormat.GithubMarkdown);
        }

        public bool IsStrategyFor(ListOptions listOptions)
        {
            return listOptions.Outdated;
        }
    }
}