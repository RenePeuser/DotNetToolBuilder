namespace Trumpf.Hmi.Uif
{
    using System.CommandLine;

    public interface IUifRootCommandBuilder
    {
        RootCommand Build();
    }
}