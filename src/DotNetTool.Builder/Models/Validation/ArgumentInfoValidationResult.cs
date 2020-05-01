namespace DotNetTool.Builder.Models.Validation
{
    internal class ArgumentInfoValidationResult : GenericValidationResult<ArgumentInfo>
    {
        public ArgumentInfoValidationResult(ArgumentInfo source, string errors) : base(source, errors)
        {
        }
    }
}