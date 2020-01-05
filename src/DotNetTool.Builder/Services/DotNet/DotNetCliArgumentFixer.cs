using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services.DotNet
{
    internal class DotNetCliArgumentFixer : IDotNetCliArgumentFixer
    {
        public string[] Fix(string[] args)
        {
            var defaultArgs = new[] { "dotnet", "newtool" };
            var newArgs = defaultArgs.Concat(args).Distinct().ToList();
            return newArgs.ToArray();
        }
    }
}