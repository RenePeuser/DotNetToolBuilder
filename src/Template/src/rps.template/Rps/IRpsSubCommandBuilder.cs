using System.CommandLine;

namespace rps.template
{
    public interface IRpsSubCommandBuilder
    {
        Command Build();
    }
}