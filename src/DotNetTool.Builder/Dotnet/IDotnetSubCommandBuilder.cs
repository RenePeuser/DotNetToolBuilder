using System.CommandLine;

namespace DotNetTool.Builder.DotNet
{
    internal interface IDotnetSubCommandBuilder
    {
        Command Build();
    }
}
