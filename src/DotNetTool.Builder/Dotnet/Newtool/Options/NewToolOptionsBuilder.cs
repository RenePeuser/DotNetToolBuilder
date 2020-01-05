using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.Dotnet.Newtool.Options
{
    internal class NewToolOptionsBuilder : INewToolOptionsBuilder
    {
        public IEnumerable<Option> Build()
        {   
            yield return BuildFromFileOption();
            yield return BuildUseVisualStudioOption();
            yield return BuildUseVsCodeOption();
        }

        private Option BuildUseVsCodeOption()
        {
            return new Option(new[] { "--use-code", "-code" }, "Opens visual studio code after generating with the new dotnet tool.")
            {
                IsHidden = true,
                Required = false
            };
        }

        private Option BuildFromFileOption()
        {
            return new Option(new[] { "--from-file", "-f" }, "the option to generate a dotnet tool from a json file")
            {
                Required = false,
                IsHidden = true,
                Argument = new Argument<System.IO.FileInfo>("fromFile")
                {
                    Description = "the file path to the dotnet tool which comes from a json file"
                }
            };
        }
        private Option BuildUseVisualStudioOption()
        {
            return new Option(new[] { "--use-visualstudio", "-ov" }, "Opens visual studio after generating the new dotnet tool.")
            {
                IsHidden = true,
                Required = false
            };
        }                                       
    }
}