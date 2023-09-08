using Argument.Check;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.SolutionBuilder.Commands
{
    internal class CommandServiceInterfaceBuilder : ICommandServiceInterfaceBuilder
    {
        private const string Template =
@"using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace $namespace$
{    
    internal interface I$command-name$Service
    {       
        Task HandleAsync($command-name$Parameters parameters);
    }
}";

        public string Build(string project, CommandInfo parameterInfo, string nameSpace)
        {
            Throw.IfNullOrWhiteSpace(project);
            Throw.IfNull(() => parameterInfo);
            Throw.IfNullOrWhiteSpace(nameSpace);

            var currentNamespace = $"{nameSpace}.Service";
            var newTemplate = Template.Replace("$command-name$", parameterInfo.NormalizedName)
                .Replace("$namespace$", currentNamespace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}
