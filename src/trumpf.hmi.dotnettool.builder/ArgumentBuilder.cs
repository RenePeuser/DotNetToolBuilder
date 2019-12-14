namespace trumpf.hmi.dotnettool.builder
{
    public class ArgumentBuilder
    {
        private const string template = 
@"namespace $project-name$
{
    using System.CommandLine;

    public class $command-name$ArgumentBuilder : I$command-name$ArgumentBuilder
    {                                        
        public Argument Build()
        {
            var argument = new Argument<object>()
            {
                Name = ""$argument-name$"",
                Description = ""$argument-description$""
            };
            
            return argument;
        }
    }
}";

        public string Build(string projectName, CliParameterInfo parameterInfo)
        {
            var newTemplate = template.Replace("$project-name$", projectName)
                .Replace("$command-name$", parameterInfo.Name.FirstCharToUpper())
                .Replace("$argument-name$", parameterInfo.ArgumentInfo.Name)
                .Replace("$argument-description$", parameterInfo.ArgumentInfo.Description);

            return newTemplate;
        }
    }
}