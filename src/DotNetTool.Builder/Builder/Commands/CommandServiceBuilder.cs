using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandServiceBuilder : ICommandServiceBuilder
    {
        private const string Template =
            @"namespace $namespace$
{
    using System;
    using System.Threading.Tasks;

    public class $command-name$Service : I$command-name$Service
    {       
        public Task HandleAsync($command-name$Parameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}";

        public string Build(string project, ParameterInfo parameterInfo, string nameSpace)
        {
            var currentNamespace = $"{nameSpace}.Service";
            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                                      .Replace("$namespace$", currentNamespace)
                                      .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
