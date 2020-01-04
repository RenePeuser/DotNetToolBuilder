using System.CommandLine;

namespace DotNetTool.Builder.NewTool
{
    public interface IDotnetCommandBuilder
    {
        RootCommand Build();
    }
}