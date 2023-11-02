using System.Linq;
using Argument.Check;

using Extensions.Pack;

namespace DotNetTool.Builder.Services.Optimizer
{
    internal sealed class DotNetToolNameNormalizer : IDotNetToolNameNormalizer
    {
        public string Normalize(string dotNetToolName)
        {
            Throw.IfNull(() => dotNetToolName);

            // sample: my-tool => MyTool. Hint we generate folder files and structures and namespace and classes
            // are not allowed to use '-'.
            var splittedToolName = dotNetToolName.Split("-").ToList();
            var splittedNormalizedToolName = splittedToolName.Select(s => s.FirstCharToUpper()).ToList();

            // if length == 1 means no '-', so return direct the normalized tool name.
            if (splittedToolName.Count == 1)
            {
                return splittedNormalizedToolName.Flatten();
            }

            var firstPart = splittedToolName.First().ToLower();

            // check that first part is not 'dotnet'. If not return normalized tool name.
            if (firstPart.NotEqualsTo("dotnet"))
            {
                return splittedNormalizedToolName.Flatten();
            }

            // here we have now the special case of 'dotnet' usage
            splittedNormalizedToolName.Remove(splittedNormalizedToolName.First());
            var trimmedDotNet = splittedNormalizedToolName.Flatten();
            return trimmedDotNet;
        }
    }
}
