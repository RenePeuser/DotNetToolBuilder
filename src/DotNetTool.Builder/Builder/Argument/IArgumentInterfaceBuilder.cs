namespace DotNetTool.Builder.Builder.Argument
{
    using Models;

    internal interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}