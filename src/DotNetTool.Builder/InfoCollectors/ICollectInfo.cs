namespace DotNetTool.Builder.InfoCollectors
{
    public interface ICollectInfo
    {
        string Title { get; }
        string Invoke();
    }
}