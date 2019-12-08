namespace Trumpf.Hmi.Uif
{
    using System.Threading.Tasks;

    class Program
    {
        static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}
