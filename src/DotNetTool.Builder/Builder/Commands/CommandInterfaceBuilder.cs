using DotNetTool.Builder.Extensions;
using DotNetTool.Builder.Models;

namespace DotNetTool.Builder.Builder.Commands
{
    internal class CommandInterfaceBuilder : ICommandInterfaceBuilder
    {
        private const string template =
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

        public string Build(string project, CliParameterInfo cliParameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}