using System.Collections.Generic;
using System.CommandLine;

namespace DotNetTool.Builder.Dotnet.Newtool.Options
{
    public class NewToolOptionsBuilder : INewToolOptionsBuilder
    {
        public IEnumerable<Option> Build()
        {   
            yield return BuildFromFileOption();
            yield return BuildOpenVisualstudioOption();
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
        private Option BuildOpenVisualstudioOption()
        {
            return new Option(new[] { "--open-visualstudio", "-ov" }, "Opens visual studio after generating the new dotnet tool.")
            {
                IsHidden = true,
                Required = false
            };
        }                                       
    }
}