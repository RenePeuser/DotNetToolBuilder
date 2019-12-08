namespace Trumpf.Hmi.Uif.Push
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Trumpf.Hmi.FileSystemAbstraction.FileSystem;
    using Trumpf.Hmi.Uif.Push.Service;
    using Trumpf.Hmi.Uif.Tcix;

    public class PushCommandBuilder : ICommandBuilder
    {
        private readonly ITcixPathService _tcixPathService;
        private readonly IPushToScapsService _pushToScapsService;

        public PushCommandBuilder(ITcixPathService tcixPathService,IPushToScapsService pushToScapsService)
        {
            _tcixPathService = tcixPathService;
            _pushToScapsService = pushToScapsService;
        }

        public Command Build()
        {
            var command = new Command("push", "Push a *.tciz file to scaps");
            command.Handler = CommandHandler.Create<TiFileInfo>(tcixPath => _pushToScapsService.PushAsync(tcixPath));
            command.AddArgument(BuildArgument());
            return command;
        }

        private Argument<TiFileInfo> BuildArgument()
        {
            var argument = new Argument<TiFileInfo>(_tcixPathService.TryConvert)
                           {
                               Name = "tcixPath",
                               Description = "The path to the tcix-file (file or folder)."
                           };

            argument.AddValidator(_tcixPathService.Validate);
            return argument;
        }
    }
}