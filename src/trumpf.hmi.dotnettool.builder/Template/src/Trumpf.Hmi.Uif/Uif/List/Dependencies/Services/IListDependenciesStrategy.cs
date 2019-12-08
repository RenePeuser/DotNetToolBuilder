namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using System.Threading.Tasks;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

    public interface IListDependenciesStrategy
    {
        Task Invoke(TiFileInfo tcixFile);

        bool IsStrategyFor(ListOptions listOptions);
    }
}