namespace DotNetTool.Builder.Models.Validation
{
    internal sealed class CommandInfoValidationResult(CommandInfo source,
                                                      string errors) : GenericValidationResult<CommandInfo>(source, errors);
}