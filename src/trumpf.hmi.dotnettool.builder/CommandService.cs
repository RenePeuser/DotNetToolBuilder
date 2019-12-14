namespace trumpf.hmi.dotnettool.builder
{
    internal class CommandService
    {
        private const string template =
@"namespace $project-name$.$command-name$

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class $command-name$Service : I$command-name$Service
    {       
        public Task HandleAsync($command-name$Parameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}";
        internal string Build(string project, CliParameterInfo cliParameterInfo)
        {
            var newTemplate = template.Replace("$command-name$", cliParameterInfo.Name.FirstCharToUpper())
                .Replace("$project-name$", project);

            return newTemplate;
        }
    }
}