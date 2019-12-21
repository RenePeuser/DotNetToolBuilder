using System.Threading.Tasks;
using DotNetTool.Builder.App;

namespace DotNetTool.Builder
{
    public class Program
    {
        public static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}