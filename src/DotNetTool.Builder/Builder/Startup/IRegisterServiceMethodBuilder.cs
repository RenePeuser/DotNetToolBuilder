namespace DotNetTool.Builder.Builder.Startup
{
    internal interface IRegisterServiceMethodBuilder
    {
        string Build(string commandName, string typeRegistrations);
    }
}
