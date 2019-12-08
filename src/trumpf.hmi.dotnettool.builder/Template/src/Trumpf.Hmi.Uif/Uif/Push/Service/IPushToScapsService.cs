namespace Trumpf.Hmi.Uif.Push.Service
{
    using System.Threading.Tasks;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

    public interface IPushToScapsService
    {
        Task<int> PushAsync(TiFileInfo tcixFileInfo);
    }
}