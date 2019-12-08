namespace Trumpf.Hmi.Uif.List.Dependencies.Services
{
    using System.Threading.Tasks;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

    public interface IListDependenciesService
    {
        Task HandleAsync(ListOptions options, TiFileInfo tcixFile);
    }
}