using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandServiceInterfaceBuilder : ICommandServiceInterfaceBuilder
    {
        private const string Template =
@"namespace $namespace$
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface I$command-name$Service
    {       
        Task HandleAsync($command-name$Parameters parameters);
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
