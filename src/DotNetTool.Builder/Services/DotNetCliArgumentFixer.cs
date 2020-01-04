using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services
{
    internal class DotNetCliArgumentFixer : IDotNetCliArgumentFixer
    {
        private string[] specialArgs = new string[] { "-v", "--version", "-h", "--help" };

        public string[] Fix(string[] args)
        {
            if (args.ContainsAny(specialArgs))
            {
                return args;
            }

            var defaultArgs = new[] { "dotnet", "newtool", "--open-visualstudio" };
            var newArgs = defaultArgs.Concat(args).Distinct().ToList();

            // we should remove all alias also, works with duplicated options, but this is more correct.
            newArgs.Remove("-ov");

            return newArgs.ToArray();
        }
    }
}