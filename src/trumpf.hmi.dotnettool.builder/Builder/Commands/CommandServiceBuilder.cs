using trumpf.hmi.dotnettool.builder.Extensions;
using trumpf.hmi.dotnettool.builder.Models;

namespace trumpf.hmi.dotnettool.builder.Builder.Commands
{
    internal class CommandServiceBuilder
    {
        private const string template =
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
        internal string Build(string project, CliParameterInfo cliParameterInfo, string nameSpace)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$namespace$", nameSpace)
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}