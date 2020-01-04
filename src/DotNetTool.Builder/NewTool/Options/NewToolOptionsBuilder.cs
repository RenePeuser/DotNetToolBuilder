using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.NewTool.Options
{
    public class NewToolOptionsBuilder : INewToolOptionsBuilder
    {
        public IEnumerable<Option> Build()
        {
            yield return BuildOpenVisualStudioOption();
        }

        private Option BuildOpenVisualStudioOption()
        {
            return new Option(new[] { "--no-visualstudio", "-nvs" }, "Do not open visual studio with new generated dotnet tool.")
            { 
                Required = false
            };
        }
    }
}
