namespace DotNetTool.Builder.InfoCollectors
{
    internal interface ICollectInfo
    {
        string Title { get; }
        string Invoke();
    }
}
