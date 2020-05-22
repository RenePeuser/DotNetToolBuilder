using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal class CommandServiceBuilder : ICommandServiceBuilder
    {
        private const string Template =
@"
using System;
using System.IO; 
using System.Threading.Tasks;

namespace $namespace$
{    
    internal class $command-name$Service : I$command-name$Service
    {       
        public Task HandleAsync($command-name$Parameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}";

        public string Build(string project, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(() => project);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(() => nameSpace);

            var currentNamespace = $"{nameSpace}.Service";
            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$namespace$", currentNamespace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
