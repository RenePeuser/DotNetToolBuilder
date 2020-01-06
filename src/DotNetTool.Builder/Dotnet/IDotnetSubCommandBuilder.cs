using System.CommandLine;

namespace DotNetTool.Builder.Dotnet
{
    internal interface IDotnetSubCommandBuilder
    {
        Command Build();
    }
}
