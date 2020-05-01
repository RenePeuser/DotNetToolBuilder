using System.CommandLine;

namespace DotNetTool.Builder.DotNet
{
    internal interface IDotnetCommandBuilder
    {
        RootCommand Build();
    }
}
