namespace Trumpf.Hmi.Uif
{
    using System.CommandLine;

    public interface IUifCommandBuilder
    {
        RootCommand Build();
    }
}