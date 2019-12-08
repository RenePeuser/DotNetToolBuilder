namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using Pastel;
    using Trumpf.Hmi.Extensions;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;
    using Trumpf.Hmi.Uif.Tcix.Models;
    using Trumpf.Hmi.Uif.Tcix.Parser;
    using YetAnotherConsoleTables;

    public class ListDependenciesStrategy : IListDependenciesStrategy
    {
        private readonly ITcixParser _tcixParser;

        public ListDependenciesStrategy(ITcixParser tcixParser)
        {
            _tcixParser = tcixParser;
        }

        public Task Invoke(TiFileInfo tcixFile)
        {
            var result = _tcixParser.Parse(tcixFile).ToList();
            var thingsToRender = result.Select(item => new
            {
                Kind = item.Kind,
                Package = item.Package,
                Expr = item.As<ExpressionVersion>()?.Expression,
                Version = item.Version.Value.Pastel(Color.LawnGreen),
            });

            ConsoleTable.From(thingsToRender).Write(new MyFormatter());
            return Task.CompletedTask;
        }

        public bool IsStrategyFor(ListOptions listOptions)
        {
            return !listOptions.Outdated && !listOptions.Minor && !listOptions.IncludePrereleases && !listOptions.Patch;
        }
    }
}