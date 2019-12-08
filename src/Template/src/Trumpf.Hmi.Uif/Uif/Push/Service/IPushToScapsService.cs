namespace Trumpf.Hmi.Uif.Push.Service
{
    using System.Threading.Tasks;

    public interface IPushToScapsService
    {
        Task<int> PushAsync();
    }
}