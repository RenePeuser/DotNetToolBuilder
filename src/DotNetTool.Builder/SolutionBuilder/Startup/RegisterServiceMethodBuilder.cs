namespace DotNetTool.Builder.SolutionBuilder.Startup
{
    internal sealed class RegisterServiceMethodBuilder : IRegisterServiceMethodBuilder
    {
        private const string RegisterServiceMethod =
            @"private static void Configure$command-name$(IServiceCollection services)
        {
$registrations$
        }";

        public string Build(string commandName, string typeRegistrations)
        {
            var newMethodSyntax = RegisterServiceMethod.Replace("$command-name$", commandName)
                .Replace("$registrations$", typeRegistrations);
            return newMethodSyntax;
        }
    }
}
