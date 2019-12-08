namespace Trumpf.Hmi.Uif.List
{
    using System.CommandLine;
    using Trumpf.Hmi.Uif.List.Dependencies;

    public class ListCommandBuilder : ICommandBuilder
    {
        private readonly IListDependenciesCommandBuilder _listDependenciesCommandBuilder;

        public ListCommandBuilder(IListDependenciesCommandBuilder listDependenciesCommandBuilder)
        {
            _listDependenciesCommandBuilder = listDependenciesCommandBuilder;
        }

        public Command Build()
        {
            var listCommand = new Command("list", "Lists the content of a tcix file.");
            listCommand.AddCommand(_listDependenciesCommandBuilder.Build());
            return listCommand;
        }
    }
}
