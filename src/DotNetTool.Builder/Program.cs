using System.Threading.Tasks;
using DotNetTool.Builder.App;

namespace DotNetTool.Builder
{
    /// <summary>The program, which represents the dot net tool builder.</summary>
    internal static class Program
    {
        /// <summary>Creates an instance of the dotnet tool builder and starts it.</summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}
