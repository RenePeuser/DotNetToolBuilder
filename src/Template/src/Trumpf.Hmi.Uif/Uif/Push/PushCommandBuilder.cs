namespace Trumpf.Hmi.Uif.Push
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using Trumpf.Hmi.Uif.Push.Service;

    public class PushCommandBuilder : ICommandBuilder
    {
        private readonly IPushToScapsService _pushToScapsService;

        public PushCommandBuilder(IPushToScapsService pushToScapsService)
        {
            _pushToScapsService = pushToScapsService;
        }

        public Command Build()
        {
            var command = new Command("push", "Push a *.tciz file to scaps");
            command.Handler = CommandHandler.Create<object>(_ =>
            {
                return _pushToScapsService.PushAsync();
            });

            command.AddArgument(BuildArgument());
            return command;
        }

        private Argument<object> BuildArgument()
        {
            return new Argument<object>();
        }
    }
}