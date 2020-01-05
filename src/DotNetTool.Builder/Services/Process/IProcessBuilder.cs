namespace DotNetTool.Builder.Services.Process
{
    internal interface IProcessBuilder
    {
        IProcess BuildFrom(string command, string arguments);
    }
}
