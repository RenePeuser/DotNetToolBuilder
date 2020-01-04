using System.CommandLine;

namespace DotNetTool.Builder.Dotnet
{
    public interface IDotnetSubCommandBuilder
    {
        Command Build();
    }
}