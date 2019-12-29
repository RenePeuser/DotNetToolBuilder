namespace DotNetTool.Builder.Builder.Parameter
{
    using Models;

    internal interface IParameterSpecificClassBuilder
    {
        string Build(string projectName, ParameterInfo parameterInfo, string nameSpace);
        bool IsThisBuilderFor(ParameterInfo parameterInfo);
    }
}