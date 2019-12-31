namespace DotNetTool.Builder.Builder.Argument
{
    using Models;

    public interface IArgumentInterfaceBuilder
    {
        string Build(string projectName, CommandInfo parameterInfo, string nameSpace);
    }
}