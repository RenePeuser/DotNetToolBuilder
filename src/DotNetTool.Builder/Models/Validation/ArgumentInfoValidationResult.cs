namespace DotNetTool.Builder.Models.Validation
{
    internal sealed class ArgumentInfoValidationResult : GenericValidationResult<ArgumentInfo>
    {
        internal ArgumentInfoValidationResult(ArgumentInfo source, string errors) : base(source, errors)
        {
        }
    }
}