namespace DotNetTool.Builder.Builder.Parameter
{
    using Extensions;
    using Models;

    internal class ParameterWithoutArgsOrOptionsClassBuilder : IParameterSpecificClassBuilder
    {
        private const string Template =
@"namespace $namespace$
{
    public class $command-name$Parameters
    {
        public $command-name$Parameters()
        {
        }
    }
}";

        public string Build(string projectName, ParameterInfo parameterInfo, string nameSpace)
        {
            var newTemplate = Template.Replace("$projectName$", projectName)
                .Replace("$namespace$", nameSpace)
                .Replace("$command-name$", parameterInfo.NormalizedName);

            return newTemplate;
        }

        public bool IsThisBuilderFor(ParameterInfo parameterInfo)
        {
            return parameterInfo.ArgumentInfo.IsNull() && parameterInfo.Options.IsNullOrEmpty();
        }
    }
}