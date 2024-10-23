using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using DotNetTool.Builder.DotNet.Newtool.Options;
using DotNetTool.Builder.DotNet.Newtool.Service;

namespace DotNetTool.Builder.DotNet.Newtool
{
    internal sealed class NewToolCommandBuilder(INewToolService newToolService,
                                                INewToolOptionsBuilder optionsBuilder) : IDotnetSubCommandBuilder
    {
        public Command Build()
        {
            var command = new Command("newtool", "creates a new dotnet tool");
            optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
            command.Handler = CommandHandler.Create<FileInfo, DirectoryInfo, bool, bool, bool, FileInfo, bool>((fromFile, saveTo, useCode, usevisualstudio, useRider, asZip, useFastMode) => newToolService.HandleAsync(new NewToolParameters(fromFile, saveTo, useCode, usevisualstudio, useRider, asZip, useFastMode)));
            return command;
        }
    }
}
