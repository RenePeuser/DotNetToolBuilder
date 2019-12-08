namespace Trumpf.Hmi.Uif.Push.Service
{
    using System;
    using System.Threading.Tasks;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;

    public class PushToScapsService : IPushToScapsService
    {
        public Task<int> PushAsync(TiFileInfo tcixFileInfo)
        {
            throw new NotImplementedException();
        }
    }
}