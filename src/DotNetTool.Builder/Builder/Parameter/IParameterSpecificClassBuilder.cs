namespace DotNetTool.Builder.Builder.Parameter
{
    using Models;

    internal interface IParameterSpecificClassBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
        bool IsThisBuilderFor(CommandInfo parameterInfo);
    }
}