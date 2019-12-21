namespace DotNetTool.Builder.Services
{
    internal interface IProcessBuilder
    {
        IProcess BuildFrom(string command, string arguments);
    }
}