using System.Collections.Generic;
using System.CommandLine;
using FileSystem.Abstraction;
using Argument = System.CommandLine.Argument;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;

namespace DotNetTool.Builder.DotNet.Newtool.Options
{
    internal class NewToolOptionsBuilder : INewToolOptionsBuilder
    {
        private readonly IFileService _fileService;

        public NewToolOptionsBuilder(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IEnumerable<Option> Build()
        {
            yield return BuildFromFileOption();
            yield return BuildSaveToolToOption();
            yield return BuildUseVisualStudioOption();
            yield return BuildUseVsCodeOption();
            yield return BuildUseCustomIDEOption();
            yield return BuildAndZipDotNetTool();
        }

        private Option BuildSaveToolToOption()
        {
            return new Option(new[] { "--save-to", "-st" }, "Saves the current dotnet tool configuration as json file to given path.") { Required = false, Argument = new Argument<DirectoryInfo>("directoryPath") { Description = "The path to the directory to save the generated dotnet tool as json" } };
        }

        private Option BuildUseCustomIDEOption()
        {
            return new Option(new[] { "--use-rider", "-ur" }, "Opens the JetBrains Rider IDE after generating with the new dotnet tool.") { Required = false };
        }

        private Option BuildUseVsCodeOption()
        {
            return new Option(new[] { "--use-code", "-uc" }, "Opens visual studio code after generating with the new dotnet tool.") { Required = false };
        }

        private Option BuildFromFileOption()
        {
            return new Option(new[] { "--from-file", "-ff" }, "Creates a dotnet tool, from an already serialized tool, which was saved as *.json") { Required = false, Argument = new Argument<FileInfo>("filePath") { Description = "the file path to the dotnet tool which comes from a json file" } };
        }

        private Option BuildUseVisualStudioOption()
        {
            return new Option(new[] { "--use-visualstudio", "-uv" }, "Opens visual studio after generating the new dotnet tool.") { Required = false };
        }

        private Option BuildAndZipDotNetTool()
        {
            return new Option(new[] { "--as-zip", "-az" }, "Generate a dotnet tool and zip is as zip file.")
            {
                Argument = new Argument<FileInfo>("zipFile") { Description = "The target zip file info." },
                Required = false
            };
        }
    }
}
