namespace trumpf.hmi.dotnettool.builder.Services
{
    internal interface IProcessBuilder
    {
        IProcess BuildFrom(string command, string arguments);
    }
}