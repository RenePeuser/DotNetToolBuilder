using System.CommandLine;
using System.IO;

namespace DotNetTool.Builder.NewTool.Arguments
{
    public class NewToolArgumentBuilder : INewToolArgumentBuilder
    {                                        
        public System.CommandLine.Argument Build()
        {
            var argument = new Argument<FileInfo>()
            {
                Name = "file",
                Description = "The file path to an already serialized dotnet tool as json."
            };
            
            return argument;
        }
    }
}