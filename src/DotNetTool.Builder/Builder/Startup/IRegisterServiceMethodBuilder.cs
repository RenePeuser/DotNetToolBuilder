namespace DotNetTool.Builder.Builder.Startup
{
    public interface IRegisterServiceMethodBuilder
    {
        string Build(string commandName, string typeRegistrations);
    }
}
