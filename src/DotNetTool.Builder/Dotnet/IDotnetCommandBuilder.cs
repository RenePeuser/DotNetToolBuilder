using System.CommandLine;

namespace DotNetTool.Builder.Dotnet
{
    internal interface IDotnetCommandBuilder
    {
        RootCommand Build();
    }
}
