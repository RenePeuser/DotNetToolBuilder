namespace DotNetTool.Builder.Models.Validation
{
    internal class CommandInfoValidationResult : GenericValidationResult<CommandInfo>
    {
        public CommandInfoValidationResult(CommandInfo source, string errors) : base(source, errors)
        {
        }
    }
}