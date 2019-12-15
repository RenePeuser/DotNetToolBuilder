using System.CommandLine;

namespace Trumpf.Hmi.Uif
{
    public interface IUifSubCommandBuilder
    {
        Command Build();
    }
}