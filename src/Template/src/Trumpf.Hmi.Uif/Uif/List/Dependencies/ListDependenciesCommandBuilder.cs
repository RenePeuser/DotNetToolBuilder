namespace Trumpf.Hmi.Uif.List.Dependencies
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Drawing;
    using Pastel;
    using Trumpf.Hmi.Uif.List.Dependencies.Services;

    public class ListDependenciesCommandBuilder : IListDependenciesCommandBuilder
    {
        private readonly IListDependenciesService _listDependenciesService;

        public ListDependenciesCommandBuilder(IListDependenciesService listDependenciesService)
        {
            _listDependenciesService = listDependenciesService;
        }

        public Command Build()
        {
            var command = new Command("dependencies", "Lists the dependencies in the specified .tcix file.");

            foreach (var option in BuildOptions())
            {
                command.AddOption(option);
            }

            command.AddArgument(BuildArgument());

            command.Handler = CommandHandler.Create<object>(_ =>
            {
                return _listDependenciesService.HandleAsync();
            });

            return command;
        }

        private Argument<object> BuildArgument()
        {
            var argument = new Argument<object>()
            {
                Name = "tcixPath",
                Description = "The path to the tcix-file (file or folder)."
            };

            // Add validator if you want to validate your argument here ->
            // argument.AddValidator();

            return argument;
        }

        private IEnumerable<Option> BuildOptions()
        {
            yield return new Option(new[] { "--outdated", "-o" }, "Lists only the dependencies that are outdated.".Pastel(Color.DimGray));
        }
    }
}

