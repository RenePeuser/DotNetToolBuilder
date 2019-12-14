namespace trumpf.hmi.dotnettool.builder
{
    internal class ICommandService
    {
        private const string template =
            @"namespace $project-name$
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface I$command-name$Service
    {       
        Task HandleAsync($command-name$Parameters parameters);
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