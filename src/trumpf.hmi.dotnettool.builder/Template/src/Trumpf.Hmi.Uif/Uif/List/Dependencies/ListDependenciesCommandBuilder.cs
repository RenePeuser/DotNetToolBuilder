namespace Trumpf.Hmi.Uif.List.Dependencies
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Drawing;
    using Pastel;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;
    using Trumpf.Hmi.Uif.List.Dependencies.Services;
    using Trumpf.Hmi.Uif.Tcix;

    public class ListDependenciesCommandBuilder : IListDependenciesCommandBuilder
    {
        private readonly IListDependenciesService _listDependenciesService;
        private readonly ITcixPathService _tcixPathService;

        public ListDependenciesCommandBuilder(IListDependenciesService listDependenciesService, ITcixPathService tcixPathService)
        {
            _listDependenciesService = listDependenciesService;
            _tcixPathService = tcixPathService;
        }

        public Command Build()
        {
            var command = new Command("dependencies", "Lists the dependencies in the specified .tcix file.");

            foreach (var option in BuildOptions())
            {
                command.AddOption(option);
            }

            command.AddArgument(BuildArgument());

            command.Handler = CommandHandler.Create<TiFileInfo, bool, bool, bool, bool>((tcixPath, outdated, minor, patch, includePrereleases) =>
            {
                var listOptions = new ListOptions(outdated, minor, patch, includePrereleases);

                return _listDependenciesService.HandleAsync(listOptions, tcixPath);
            });

            return command;
        }

        private Argument<TiFileInfo> BuildArgument()
        {
            var argument = new Argument<TiFileInfo>(_tcixPathService.TryConvert)
            {
                Name = "tcixPath",
                Description = "The path to the tcix-file (file or folder)."
            };

            argument.AddValidator(_tcixPathService.Validate);
            return argument;
        }

        private IEnumerable<Option> BuildOptions()
        {
            yield return new Option(new[] { "--outdated", "-o" }, "Lists only the dependencies that are outdated.".Pastel(Color.DimGray));
            yield return new Option(new[] { "--highest-minor", "-m" }, "Must be combined with '--outdated'. Lists dependencies that have the same major version but have higher minor and/or patch versions.".Pastel(Color.DimGray));
            yield return new Option(new[] { "--highest-patch", "-p" }, "Must be combined with '--outdated'. Lists dependencies that have the same major and minor version but have a higher patch version.".Pastel(Color.DimGray));
            yield return new Option(new[] { "--include-prereleases", "-i" }, "Includes dependencies that are marked as prerelease.".Pastel(Color.DimGray));
        }
    }
}

