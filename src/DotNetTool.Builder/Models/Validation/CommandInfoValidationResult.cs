namespace DotNetTool.Builder.Models.Validation
{
    internal sealed class CommandInfoValidationResult : GenericValidationResult<CommandInfo>
    {
        public CommandInfoValidationResult(CommandInfo source, string errors) : base(source, errors)
        {
        }
    }
}