using System.CommandLine;

namespace DotNetTool.Builder.Dotnet
{
    public interface IDotnetCommandBuilder
    {
        RootCommand Build();
    }
}