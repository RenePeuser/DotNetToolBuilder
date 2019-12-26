namespace rps.template
{
    using System.CommandLine;

    public interface IRpsCommandBuilder
    {
        RootCommand Build();
    }
}