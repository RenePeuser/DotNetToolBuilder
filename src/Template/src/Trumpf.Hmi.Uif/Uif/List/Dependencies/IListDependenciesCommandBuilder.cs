namespace Trumpf.Hmi.Uif.List.Dependencies
{
    using System.CommandLine;

    public interface IListDependenciesCommandBuilder
    {
        Command Build();
    }
}