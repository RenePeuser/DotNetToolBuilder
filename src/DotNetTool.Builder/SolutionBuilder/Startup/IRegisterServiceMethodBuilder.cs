namespace DotNetTool.Builder.SolutionBuilder.Startup
{
    internal interface IRegisterServiceMethodBuilder
    {
        string Build(string commandName, string typeRegistrations);
    }
}
