namespace Trumpf.Hmi.Uif
{
    using System.CommandLine;

    public interface ICommandBuilder
    {
        Command Build();
    }
}
