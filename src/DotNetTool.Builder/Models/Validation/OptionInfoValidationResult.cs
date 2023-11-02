namespace DotNetTool.Builder.Models.Validation
{
    internal sealed class OptionInfoValidationResult : GenericValidationResult<OptionInfo>
    {
        public OptionInfoValidationResult(OptionInfo source, string errors) : base(source, errors)
        {
        }
    }
}