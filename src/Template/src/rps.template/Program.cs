namespace rps.template
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
