using DotNetTool.Builder.Services;

namespace DotNetTool.Builder.InfoCollectors
{
    public abstract class CollectInfoStep : ICollectInfo
    {
        private readonly IConsoleService _consoleService;

        protected CollectInfoStep(IConsoleService consoleService, string title)
        {
            _consoleService = consoleService;
            Title = title;
        }

        public string Title { get; }

        public virtual string Invoke()
        {
            _consoleService.WriteLine(Title);
            return _consoleService.ReadLine();
        }
    }
}