using System.Linq;
using DotNetTool.Builder.Extensions;

namespace DotNetTool.Builder.Services.DotNet
{
    internal class DotNetCliArgumentFixer : IDotNetCliArgumentFixer
    {
        private string[] specialArgs = new[] { "-v", "--version", "-h", "--help" };

        public string[] Fix(string[] args)
        {
            if (args.ContainsAny(specialArgs))
            {
                return args;
            }

            var defaultArgs = new[] { "dotnet", "newtool", "--use-visualstudio" };
            var newArgs = defaultArgs.Concat(args).Distinct().ToList();

            // we should remove all alias also, works with duplicated options, but this is more correct.
            newArgs.Remove("-ov");

            return newArgs.ToArray();
        }
    }
}